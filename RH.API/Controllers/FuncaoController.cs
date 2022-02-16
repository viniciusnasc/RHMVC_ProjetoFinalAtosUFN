﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.Dtos.Input;
using RH.Domain.Dtos.Views;
using RH.Domain.Interfaces.Services;

namespace RH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncaoController : ControllerBase
    {
        private readonly IFuncaoService _funcaoService;

        public FuncaoController(IFuncaoService funcaoService)
        {
            _funcaoService = funcaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncaoViewDtoResult>>> FindAll()
        {
            var departamentos = await _funcaoService.BuscarTodos();
            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncaoViewDtoResult>> FindById(Guid id)
        {
            var departamento = await _funcaoService.BuscarPorIdAsync(id);
            return Ok(departamento);
        }

        [HttpGet("Funcionarios/{id}")]
        public async Task<ActionResult<FuncaoViewDtoResult>> ListarFuncionariosPorFuncao(Guid id)
        {
            var funcs = await _funcaoService.ListarFuncFuncaoAsync(id);
            return Ok(funcs);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FuncaoCadastroDto dto)
        {
            if (dto == null)
                return BadRequest();

            await _funcaoService.CadastrarAsync(dto);
            return Ok("Funcao cadastrada com sucesso");
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid id, FuncaoEditarDto dto)
        {
            if (dto == null)
                return BadRequest();

            await _funcaoService.AtualizarAsync(id, dto);
            return Ok("Funcao atualizada com sucesso");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> AumentarSalarioFuncao(Guid id, FuncaoEditarSalarioDto dto)
        {
            await _funcaoService.AumentarSalarioAsync(id,dto);
            return Ok("Aumentado o salário da funcão");
        }

        [HttpPatch]
        public async Task<ActionResult> AumentarTodosSalarios(FuncaoAumentoSalarialDto dto)
        {
            await _funcaoService.RealizarAumentoAsync(dto);
            return Ok("Aumentado todos os salarios");
        }
    }
}