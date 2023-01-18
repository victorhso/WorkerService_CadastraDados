using WorkerService.CadastraDados.Domain.Model;

namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface ITelefoneRepository
    {
        void InsertTelefone(Telefone telefone);
    }
}
