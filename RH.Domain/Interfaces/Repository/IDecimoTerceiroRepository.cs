﻿using RH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Domain.Interfaces.Repository
{
    public interface IDecimoTerceiroRepository : IBaseRepository<DecimoTerceiro>
    {
        Task<DecimoTerceiro> BuscaPrimeiraParcelaAsync(Guid funcionarioid, int ano);
        Task<List<DecimoTerceiro>> PegarTodosDecimosDataAsync(DateTime dataPagamento);
        Task<List<DecimoTerceiro>> BuscaTodasParcelasAnoAsync(Guid funcionarioid, int ano);
    }
}
