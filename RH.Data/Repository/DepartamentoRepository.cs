﻿using Microsoft.EntityFrameworkCore;
using RH.Data.Contexto;
using RH.Domain.Entities;
using RH.Domain.Interfaces.Repository;

namespace RH.Data.Repository
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(RhContext context) : base(context) { }

        public async Task<int> QuantidadeFuncionarioAsync(Guid id)
        {
            return await _context.Funcionarios.CountAsync(x => x.DepartamentoId == id && x.Ativo == true);
        }

        public IEnumerable<Funcionario> BuscarFuncDepto(Guid id)
        {
            return _context.Funcionarios.Where(x => x.DepartamentoId == id && x.Ativo == true);
        }

        public async Task<bool> ExisteDepto(string depto, string subdepto)
        {
            return await _context.Departamentos.AnyAsync(x => x.NomeDepartamento == depto && x.SubDepartamento == subdepto);
        }
    }
}