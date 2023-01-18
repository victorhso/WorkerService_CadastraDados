using Microsoft.EntityFrameworkCore;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Model;
using WorkerService.CadastraDados.Domain.Repository;

namespace WorkerService.CadastraDados.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        protected readonly DbContextOptions<Context> _DbOptions;
        protected readonly ConnStrings _connStrings;

        public EnderecoRepository(Context context, ConnStrings connStrings)
        {
            _connStrings = connStrings;
            _DbOptions = new DbContextOptionsBuilder<Context>().UseSqlServer(_connStrings.SqlConnectionString).Options;
        }

        public void InsertEndereco(Endereco endereco)
        {
            using (var Db = new Context(_DbOptions))
            {
                Db.Add(endereco);
                Db.SaveChanges();
            }
        }
    }
}
