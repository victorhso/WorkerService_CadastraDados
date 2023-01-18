using WorkerService.CadastraDados.Domain.Model;

namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface IPessoaRepository
    {
        int InsertPessoa(Pessoa pessoa);
    }
}
