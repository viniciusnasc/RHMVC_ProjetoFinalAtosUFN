﻿using Estoque.Domain.Dto;
using Estoque.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EpiController : ControllerBase
    {
        private readonly IEpiService _epiService;

        public EpiController(IEpiService epiService)
        {
            _epiService = epiService ?? throw new ArgumentNullException(nameof(epiService)); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpiDto>>> FindAllAsync()
        {
            var epis = await _epiService.BuscarTodos();
            return Ok(epis);
        }

      /*  [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoViewDtoResult>> FindById(Guid id)
        {
            var departamento = await _departamentoService.BuscarPorIdAsync(id);
            return Ok(departamento);
        }*/

        [HttpPost]
        public async Task<ActionResult> CreateAsync(EpiCadastrarDto dto)
        {
            if (dto == null)
                return BadRequest();

            var result = await _epiService.Cadastrar(dto);
            return Ok(result);
        }

        /*
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult> Update(DepartamentoEditarDto dto)
        {
            if (dto == null)
                return BadRequest();

            var result = await _departamentoService.AtualizarAsync(dto);
            return Ok(result);
        }*/
    }
}
