﻿using AutoMapper;
using Refit;
using RH.Domain.Dtos.Input;
using RH.Domain.Dtos.Responses;
using RH.Domain.Dtos.Views;
using RH.Domain.Entities;
using RH.Domain.Exceptions;
using RH.Domain.Interfaces.Repository;
using RH.Domain.Interfaces.Services;
using RH.Domain.Menssagem;

namespace RH.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FuncionarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FuncionarioViewDtoResult> BuscarPorId(Guid id)
        {
            var funcEntity = await _unitOfWork.FuncionarioRepository.SelecionarPorId(id);
            return _mapper.Map<FuncionarioViewDtoResult>(funcEntity);
        }

        public async Task<List<FuncionarioViewDtoResult>> BuscarTodosAtivosAsync()
        {
            var entities = _unitOfWork.FuncionarioRepository.BuscarTodosAtivos();
            var dtos = _mapper.Map<List<FuncionarioViewDtoResult>>(entities);
            foreach(var item in dtos)
            {
                await AtribuirDepto(item);
                await AtribuirFuncao(item);
            }
            return dtos.OrderBy(x => x.Registro).ToList();
        }

        public async Task<AdmissaoMessage> CadastrarFuncionarioAsync(FuncionarioCadastroDto dto)
        {
            var funcionarioExiste = await _unitOfWork.FuncionarioRepository.ProcurarPorCpfAtivoAsync(dto.Cpf);

            if (funcionarioExiste != null)
                throw new FuncionarioJaExisteException();

            var emailExiste = await _unitOfWork.FuncionarioRepository.BuscarPorEmailAsync(dto.Email);

            if (emailExiste != null)
                throw new EmailJaCadastradoException();

            var entity = _mapper.Map<Funcionario>(dto);
            entity.Registro = await _unitOfWork.FuncionarioRepository.AtribuirNumeroDeRegistroAsync();
            entity.EnderecoId = await CadastrarEnderecoAsync(dto);
            entity.ContaBancariaId = await CadastrarContaBancariaAsync(dto);
            var mensagem = _mapper.Map<AdmissaoMessage>(entity);
            await _unitOfWork.FuncionarioRepository.Incluir(entity);
            mensagem.Id = (await _unitOfWork.FuncionarioRepository.ProcurarPorCpfAtivoAsync(entity.CPF)).Id;
            mensagem.Departamento = (await _unitOfWork.DepartamentoRepository.SelecionarPorId(entity.DepartamentoId)).ToString();
            mensagem.Funcao = (await _unitOfWork.FuncaoRepository.SelecionarPorId(entity.FuncaoId)).NomeFuncao;
            mensagem.AssuntoEmail = "Admissao";
            return mensagem;
        }

        private async Task<Guid> CadastrarContaBancariaAsync(FuncionarioCadastroDto dto)
        {
            var contaExiste = await _unitOfWork.ContaBancariaRepository.BuscarContaAsync(dto.Banco, dto.Agencia, dto.ContaCorrente);

            if(contaExiste == null)
            {
                var entity = _mapper.Map<ContaBancaria>(dto);
                await _unitOfWork.ContaBancariaRepository.Incluir(entity);
                return entity.Id;
            }
            return contaExiste.Id;
        }

        private async Task<Guid> CadastrarEnderecoAsync(FuncionarioCadastroDto dto)
        {
            var enderecoExiste = await _unitOfWork.EnderecoRepository.ProcurarEndereco(dto.Endereco.Cep, dto.Endereco.Numero, dto.Endereco.Complemento);

            if (enderecoExiste == null)
            {
                var cepResponse = await AtribuirEndereco(dto);

                var entity = _mapper.Map<Endereco>(dto.Endereco);
                entity.Complemento = "";
                entity.Rua = cepResponse.Logradouro;
                entity.MunicipioId = await CadastrarMunicipioAsync(dto);
                await _unitOfWork.EnderecoRepository.Incluir(entity);
                return entity.Id;
            }
            else
                return enderecoExiste.Id;
        }

        private async Task<Guid> CadastrarMunicipioAsync(FuncionarioCadastroDto dto)
        {
            var cepResponse = await AtribuirEndereco(dto);
            var ufId = await _unitOfWork.UfRepository.BuscarIdPorUfAsync(cepResponse.Uf);
            var entity = await _unitOfWork.MunicipioRepository.BuscarPorNomeUfAsync(cepResponse.Localidade, ufId);

            if (entity == null)
            {
                Municipio entityMun = _mapper.Map<Municipio>(cepResponse);
                entityMun.UfId = ufId;
                await _unitOfWork.MunicipioRepository.Incluir(entityMun);
                return entityMun.Id;
            }
            else
                return entity.Id;
        }

        public async Task EditarDadosPessoaisAsync(Guid id, FuncionarioEditarDadosPessoaisDto dto)
        {
            var entity = await _unitOfWork.FuncionarioRepository.SelecionarPorId(dto.Id);

            entity = _mapper.Map(dto, entity);

            await _unitOfWork.FuncionarioRepository.Alterar(entity);
        }

        public async Task Demitir(Guid id, DateTime demissao)
        {
            var entity = await _unitOfWork.FuncionarioRepository.SelecionarPorId(id);
            if (entity.Ativo)
            {
                if (entity.Admissao > demissao)
                    throw new DemissaoException();

                else
                {
                    entity.Ativo = false;
                    entity.DataDemissao = demissao;
                    await _unitOfWork.FuncionarioRepository.Alterar(entity);
                }
            }
            else
                throw new FuncionarioJaEstaDemitidoException();
        }

        private async Task<CepResponse> AtribuirEndereco(FuncionarioCadastroDto dto)
        {
            var cepClient = RestService.For<ICepApiService>("https://viacep.com.br");
            var cepResponse = await cepClient.GetAdressAsync(dto.Endereco.Cep);
            return cepResponse;
        }

        private async Task AtribuirDepto(FuncionarioViewDtoResult dto)
        {
            dto.Departamento = (await _unitOfWork.DepartamentoRepository.SelecionarPorId(dto.DepartamentoId)).NomeDepartamento;
        }

        private async Task AtribuirFuncao(FuncionarioViewDtoResult dto)
        {
            dto.Funcao = (await _unitOfWork.FuncaoRepository.SelecionarPorId(dto.FuncaoId)).NomeFuncao;
        }

        public async Task<FuncionarioViewDtoResult> ProcurarPorEmailAsync(string email)
        {
            var entity = await _unitOfWork.FuncionarioRepository.BuscarPorEmailAsync(email);
            return _mapper.Map<FuncionarioViewDtoResult>(entity);
        }
    }
}