﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Domain.Dtos.Input
{
    public class FuncaoCadastroDto
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome de função deve ter no minimo 4 e no máximo 50 letras!")]
        [Display(Name = "Função")]
        [Required(ErrorMessage = "É necessário informar o nome da função!")]
        public string NomeFuncao { get; set; }

        [Display(Name = "Salário")]
        [Required(ErrorMessage = "É necessário informar o valor do salário da função!")]
        public double Salario { get; set; }
    }
}
