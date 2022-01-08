﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Domain.Entities
{
    public class Demissao : BaseEntity
    {
        public DateTime DataPagamento { get; set; }
        public double ValorMes { get; set; }
        public double ValorDecimo { get; set; }
        public double ValorFerias { get; set; }
        public Guid FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
