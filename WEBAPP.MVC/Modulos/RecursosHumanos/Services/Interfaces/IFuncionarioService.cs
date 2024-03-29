﻿using WEBAPP.MVC.Modulos.RecursosHumanos.Models;
using WEBAPP.MVC.Modulos.RecursosHumanos.Models.InputModel;

namespace WEBAPP.MVC.Modulos.RecursosHumanos.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<List<FuncionarioModel>> BuscarTodosAtivos(string accessToken);
        Task<FuncionarioModel> FindById(Guid id, string accessToken);
        Task<FuncionarioModel> Create(FuncionarioCadastro dto, string accessToken);
        Task Update(FuncionarioUpdate model, string accessToken);
    }
}