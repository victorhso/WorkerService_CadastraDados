using WorkerService.CadastraDados.Domain.Dtos;

namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface IHttpFactoryService
    {
        Task<ResponseBuscaEnderecoPorCepDto> BuscarEnderecoPorCep(string cep);
    }
}
