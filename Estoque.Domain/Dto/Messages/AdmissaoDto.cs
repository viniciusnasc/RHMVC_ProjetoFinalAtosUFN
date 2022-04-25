﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.Domain.Dto.Messages
{
    public class AdmissaoDto
    {
        public Guid Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; }
        public string Departamento { get; set; }
    }
}
