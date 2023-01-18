using Microsoft.Extensions.DependencyInjection;
using WorkerService.CadastraDados.Domain.Repository;

namespace WorkerService.CadastraDados.Repository.Microsoft.Extensions.DependencyInjection
{
    public static class DbRepositoryAdapterServiceCollectionsExtensions
    {
        public static IServiceCollection AddDbRepositoryAdapter(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IRabbitMQRepository, RabbitMQRepository>();
            services.AddSingleton<IPessoaRepository, PessoaRepository>();
            services.AddSingleton<ITelefoneRepository, TelefoneRepository>();
            services.AddSingleton<IEnderecoRepository, EnderecoRepository>();

            return services;
        }
    }
}
