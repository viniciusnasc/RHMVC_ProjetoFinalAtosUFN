﻿namespace RH.Domain.Entities
{
    public class Funcao : BaseEntity
    {
        public string NomeFuncao { get; set; }
        public double Salario { get; set; }
        public IEnumerable<Funcionario> Funcionarios { get; set; }

        public override string ToString()
        {
            return NomeFuncao;
        }
    }
}