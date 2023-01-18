using Microsoft.EntityFrameworkCore;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Model;
using WorkerService.CadastraDados.Domain.Repository;

namespace WorkerService.CadastraDados.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        protected readonly DbContextOptions<Context> _DbOptions;
        protected readonly ConnStrings _connStrings;

        public PessoaRepository(Context context, ConnStrings connStrings)
        {
            _connStrings = connStrings;
            _DbOptions = new DbContextOptionsBuilder<Context>().UseSqlServer(_connStrings.SqlConnectionString).Options;
        }

        public int InsertPessoa(Pessoa pessoa)
        {
            using (var Db = new Context(_DbOptions))
            {
                Db.Add(pessoa);

                int identity = Db.SaveChanges();
                return identity;
            }
        }
    }
}
