using WorkerService.CadastraDados.Domain.Dtos;

namespace WorkerService.CadastraDados.Domain.Services
{
    public interface ICadastrarDadosService
    {
        Task CadastrarDados(DadosCadastroDto dadosCadastroDto);
    }
}
