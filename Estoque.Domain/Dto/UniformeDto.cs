﻿namespace Estoque.Domain.Dto
{
    public class UniformeDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
    }
}
