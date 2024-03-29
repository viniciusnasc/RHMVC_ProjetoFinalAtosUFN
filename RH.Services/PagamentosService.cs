﻿using RH.Domain.Dtos.Input;
using RH.Domain.Dtos.Views;
using RH.Domain.Entities;
using RH.Domain.Exceptions;
using RH.Domain.Interfaces.Repository;
using RH.Domain.Interfaces.Services;

namespace RH.Services
{
    public class PagamentosService : IPagamentosService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PagamentosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task GerarFolhaPagamentoAsync(DateTime dataPagamento)
        {
            var funcionarios = await _unitOfWork.FuncionarioRepository.SelecionarTudo();

            foreach (var func in funcionarios)
            {
                Pagamento pagamento = new(dataPagamento, func.Id);
                pagamento.Valor = await CalcularSalarioMes(dataPagamento, func);

                if (await _unitOfWork.PagamentoRepository.VerificaSeExistePagamentoAsync(dataPagamento, func.Id))
                    pagamento.Valor = 0;

                if (pagamento.Valor != 0)
                    await _unitOfWork.PagamentoRepository.Incluir(pagamento);
            }

            ExportadorService helper = new(_unitOfWork);
            await helper.CriarArquivoPagamento("pagamento", dataPagamento);
        }

        public async Task GerarDecimoTerceiroAsync(DateTime dataPagamento)
        {
            var funcionarios = await _unitOfWork.FuncionarioRepository.SelecionarTudo();
            foreach (var func in funcionarios)
            {
                DecimoTerceiro decimoTerceiro = new(dataPagamento, func.Id);

                // Se estiver sendo gerado em dezembro, sera considerado segunda parcela
                if (dataPagamento.Month != 12)
                    decimoTerceiro.Valor = await CalcularDecimo(func, dataPagamento, 1);
                else
                    decimoTerceiro.Valor = await CalcularDecimo(func, dataPagamento, 2);

                if (decimoTerceiro.Valor != 0 && func.Ativo == true)
                    await _unitOfWork.DecimoTerceiroRepository.Incluir(decimoTerceiro);
            }

            ExportadorService helper = new(_unitOfWork);
            await helper.CriarArquivoDecimo("Decimo terceiro", dataPagamento);
        }

        private async Task<double> CalcularDecimo(Funcionario func, DateTime pagamento, int parcela)
        {
            int quantidade;
            int anoAdmissao = func.Admissao.Year;
            int mesAdmissao = func.Admissao.Month;
            int diaAdmissao = func.Admissao.Day;
            Funcao funcao = await _unitOfWork.FuncaoRepository.SelecionarPorId(func.FuncaoId);

            int diasMesAdmissao = DateTime.DaysInMonth(anoAdmissao, mesAdmissao);

            if (anoAdmissao > pagamento.Year)
                quantidade = 0;

            else if (anoAdmissao != pagamento.Year)
                quantidade = 12;

            else if (diasMesAdmissao - diaAdmissao >= 15)
                quantidade = 13 - mesAdmissao;

            else
                quantidade = 12 - mesAdmissao;

            if (func.Ativo == false)
            {
                if (await _unitOfWork.DemissaoRepository.ExistePagamentoDemissaoAsync(func.Id))
                    quantidade = 0;

                if (((DateTime)func.DataDemissao).Day > 15)
                    quantidade += ((DateTime)func.DataDemissao).Month - 12;

                else
                    quantidade += ((DateTime)func.DataDemissao).Month - 13;
            }

            if (parcela == 1)
            {
                var primeiraParcela = await _unitOfWork.DecimoTerceiroRepository.BuscaPrimeiraParcelaAsync(func.Id, pagamento.Year);

                if (primeiraParcela == null)
                    return (funcao.Salario / 12 * quantidade) / 2;

                else
                    return 0;
            }

            else
            {
                var primeiraParcela = await _unitOfWork.DecimoTerceiroRepository.BuscaPrimeiraParcelaAsync(func.Id, pagamento.Year);
                if (primeiraParcela == null)
                    return (funcao.Salario / 12 * quantidade);

                else
                {
                    var todos = await _unitOfWork.DecimoTerceiroRepository.BuscaTodasParcelasAnoAsync(func.Id, pagamento.Year);
                    double valor = (funcao.Salario / 12 * quantidade);

                    foreach (var pag in todos)
                    {
                        valor -= pag.Valor;
                    }
                    return valor;
                }
            }
        }

        public async Task GerarFeriasAsync(DateTime dataPagamento, Guid idFunc)
        {
            var funcionario = await _unitOfWork.FuncionarioRepository.SelecionarPorId(idFunc);

            Ferias ferias = new(dataPagamento, idFunc);
            ferias.Valor = await CalcularFerias(funcionario, dataPagamento);

            if (ferias.Valor != 0)
            {
                await _unitOfWork.FeriasRepository.Incluir(ferias);
                ExportadorService helper = new(_unitOfWork);
                await helper.CriarArquivoFerias("Ferias", dataPagamento, funcionario);
            }
        }

        private async Task<double> CalcularFerias(Funcionario func, DateTime data)
        {
            var numFeriasGozadas = await _unitOfWork.FeriasRepository.VerificarQuantidadeFeriasRecebidasAsync(func.Id);

            int quantidadeDireito = 0;
            int anoAdmissao = func.Admissao.Year;
            int mesAdmissao = func.Admissao.Month;
            int diaAdmissao = func.Admissao.Day;
            Funcao funcao = await _unitOfWork.FuncaoRepository.SelecionarPorId(func.FuncaoId);

            if (func.DataDemissao == null)
            {
                // Verifica o inicio das férias do funcionario
                DateTime primeiroDiaMesFerias = data.Month == 12 ?
                                     new DateTime(data.Year + 1, 1, 1) :
                                     new DateTime(data.Year, data.Month + 1, 1);

                var ultimoDiaTrabalhado = primeiroDiaMesFerias.AddDays(-1);

                // Verificar se o funcionário já tem um ano de servico
                if ((ultimoDiaTrabalhado.Subtract(func.Admissao)).TotalDays < 365)
                    throw new SemDireitoAFeriasException();

                var totalMesesTrabalhados = Math.Truncate(ultimoDiaTrabalhado.Subtract(func.Admissao).Days / (365.25 / 12));
                var totalMesesPendentes = totalMesesTrabalhados - numFeriasGozadas * 12;

                // Verifica se o funcionário tem um periodo aquisitivo completo
                if (totalMesesPendentes < 12)
                    throw new SemDireitoAFeriasException();

                return funcao.Salario + funcao.Salario / 3;
            }
            else if (func.DataDemissao != null)
            {
                var ultimoDiaTrabalhado = (DateTime)func.DataDemissao;
                var totalMesesTrabalhados = Math.Truncate(ultimoDiaTrabalhado.Subtract(func.Admissao).Days / (365.25 / 12));
                var totalMesesPendentes = totalMesesTrabalhados - numFeriasGozadas * 12;
                return funcao.Salario + funcao.Salario / 3;
            }
            return quantidadeDireito * funcao.Salario;
        }

        public async Task CalcularDemissao(Guid id)
        {
            var funcionario = await _unitOfWork.FuncionarioRepository.SelecionarPorId(id);

            if (funcionario.DataDemissao != null)
            {
                Demissao demissao = new();
                demissao.FuncionarioId = id;
                demissao.DataPagamento = ((DateTime)funcionario.DataDemissao).AddDays(10);
                demissao.ValorMes = await CalcularSalarioRescisaoAsync(funcionario);
                demissao.ValorDecimo = await CalcularDecimo(funcionario, (DateTime)funcionario.DataDemissao, 2);
                demissao.ValorFerias = await CalcularFerias(funcionario, (DateTime)funcionario.DataDemissao);
                await _unitOfWork.DemissaoRepository.Incluir(demissao);
                ExportadorService helper = new(_unitOfWork);
                await helper.CriarArquivoDemissao("Demissao", ((DateTime)funcionario.DataDemissao).AddDays(10), funcionario);
            }
            else
                throw new Exception();
        }

        private async Task<double> CalcularSalarioRescisaoAsync(Funcionario funcionario)
        {
            var salario = (await _unitOfWork.FuncaoRepository.SelecionarPorId(funcionario.FuncaoId)).Salario;
            var dataDemissao = (DateTime)funcionario.DataDemissao;

            DateTime primeiroDiaMesDemissao = dataDemissao.Month == 1 ?
                                         new DateTime(dataDemissao.Year - 1, 12, 1) :
                                         new DateTime(dataDemissao.Year, dataDemissao.Month - 1, 1);

            if (funcionario.Admissao > primeiroDiaMesDemissao)
            {
                var totalDias = dataDemissao.Subtract(funcionario.Admissao).TotalDays;
                return salario / 30 * totalDias;
            }
            else
                return salario / 30 * dataDemissao.Day;
        }

        private async Task<double> CalcularSalarioMes(DateTime dataPagamento, Funcionario funcionario)
        {
            var salario = (await _unitOfWork.FuncaoRepository.SelecionarPorId(funcionario.FuncaoId)).Salario;

            DateTime primeiroDiaMesTrabalhado = dataPagamento.Month == 1 ?
                                     new DateTime(dataPagamento.Year - 1, 12, 1) :
                                     new DateTime(dataPagamento.Year, dataPagamento.Month - 1, 1);
            DateTime ultimoDiaMesTrabalhado = new DateTime(primeiroDiaMesTrabalhado.Year,
                                                           primeiroDiaMesTrabalhado.Month,
                                                           DateTime.DaysInMonth(primeiroDiaMesTrabalhado.Year, primeiroDiaMesTrabalhado.Month));

            // Verifica se o funcionário está de férias. No caso, subtraio dois pois ele busca a data de pagamento,
            // que é um mês antes ao periodo de férias.
            var ferias = await _unitOfWork.FeriasRepository.BuscarFeriasMesAsync(dataPagamento.AddMonths(-2), funcionario.Id);
            if (ferias != null)
                return 0;

            // Verifica se o funcionário está demitido
            if (funcionario.DataDemissao != null)
            {
                // Se a data de demissão for no mês do pagamento, calcula o proporcional
                if (((DateTime)funcionario.DataDemissao).Month == dataPagamento.Month && ((DateTime)funcionario.DataDemissao).Year == dataPagamento.Year)
                    return salario / 30 * ((DateTime)funcionario.DataDemissao).Day;

                return 0;
            }

            // Caso já conste pagamento na base de dados, retornará 0
            if (await _unitOfWork.PagamentoRepository.VerificaSeExistePagamentoAsync(dataPagamento, funcionario.Id))
                return 0;

            // Se o funcionario foi admitido antes do mes em questao, retorna o salario completo
            if (funcionario.Admissao < primeiroDiaMesTrabalhado)
                return salario;

            // Calcula salário caso o funcionário tenha entrado no mês corrente
            else if (funcionario.Admissao > primeiroDiaMesTrabalhado)
            {
                var diasNoMes = ultimoDiaMesTrabalhado.Day;
                var diaInicio = funcionario.Admissao.Day;
                var diasTrabalhados = diasNoMes - diaInicio + 1;
                return salario / 30 * diasTrabalhados;
            }

            return 0;
        }
    }
}