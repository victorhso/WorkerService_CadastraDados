using WorkerService.CadastraDados.Domain.Model;

namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface IEnderecoRepository
    {
        void InsertEndereco(Endereco endereco);
    }
}
