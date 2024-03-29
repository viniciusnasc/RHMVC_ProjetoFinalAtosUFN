﻿using Microsoft.EntityFrameworkCore;
using RH.Data.Contexto;
using RH.Domain.Entities;
using RH.Domain.Interfaces.Repository;

namespace RH.Data.Repository
{
    public class PagamentoRepository : BaseRepository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(RhContext context) : base(context)
        {}

        public async Task<bool> VerificaSeExistePagamentoAsync(DateTime dataPagamento, Guid idFuncionario)
        {
           return await _context.Pagamentos.AnyAsync(x => x.FuncionarioId == idFuncionario && 
                                                         x.DataPagamento.Month == dataPagamento.Month && 
                                                         x.DataPagamento.Year == dataPagamento.Year);
        }

        public async Task<List<Pagamento>> PegarTodosPagamentosDataAsync(DateTime dataPagamento)
        {
            return await _context.Pagamentos.Where(x => x.DataPagamento.Month == dataPagamento.Month && 
                                                       x.DataPagamento.Year == dataPagamento.Year)
                                           .Include(x => x.Funcionario)
                                           .ThenInclude(x => x.ContaBancaria).ToListAsync();
        }
    }
}