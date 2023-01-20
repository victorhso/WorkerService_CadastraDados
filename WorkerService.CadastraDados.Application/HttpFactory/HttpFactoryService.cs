using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Repository;

namespace WorkerService.CadastraDados.Application.HttpFactory
{
    public class HttpFactoryService : IHttpFactoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string BaseUri = "https://viacep.com.br/ws";

        public async Task<ResponseBuscaEnderecoPorCepDto> BuscarEnderecoPorCep(string cep)
        {
            string result = String.Empty;
            string url = string.Format("{0}/{1}", BaseUri, $"{cep}/json");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();

                var BuscaEnderecoPorCepDto = JsonConvert.DeserializeObject<ResponseBuscaEnderecoPorCepDto>(result);
                return BuscaEnderecoPorCepDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao realizar a consulta na URL {url} - #Exception: {ex.Message}");
            }
        }
    }
}
