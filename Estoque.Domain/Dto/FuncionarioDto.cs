﻿namespace Estoque.Domain.Dto
{
    public class FuncionarioDto
    {
        public Guid Id { get; set; }
        public int Registro { get; set; }
        public string? Nome { get; set; }
        public string? Funcao { get; set; }
        public string? Departamento { get; set; }
    }
}
