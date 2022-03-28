﻿using System.Net.Http.Headers;
using WEBAPP.MVC.Models;
using WEBAPP.MVC.Models.InputModel;
using WEBAPP.MVC.Services.IServices;
using WEBAPP.MVC.Utils;

namespace WEBAPP.MVC.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/Funcionario";

        public FuncionarioService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<List<FuncionarioModel>> BuscarTodosAtivos(string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<FuncionarioModel>>();
        }

        public async Task<FuncionarioModel> FindById(Guid id, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<FuncionarioModel>();
        }

        public async Task<FuncionarioModel> Create(FuncionarioCadastro dto, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PostAsJson(BasePath, dto);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<FuncionarioModel>();

            else
                throw new Exception("Something went wrong when calling API");
        }

        public async Task Update(FuncionarioUpdate model, string accessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _client.PutAsJson(BasePath, model);
            if (response.IsSuccessStatusCode)
                return;

            else
                throw new Exception("Something went wrong when calling API");
        }
    }
}
