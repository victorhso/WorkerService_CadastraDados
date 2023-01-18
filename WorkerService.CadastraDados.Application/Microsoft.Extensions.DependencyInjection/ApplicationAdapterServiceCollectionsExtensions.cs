using Microsoft.Extensions.DependencyInjection;
using WorkerService.CadastraDados.Application.HttpFactory;
using WorkerService.CadastraDados.Domain.Repository;
using WorkerService.CadastraDados.Domain.Services;

namespace WorkerService.CadastraDados.Application.Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationAdapterServiceCollectionsExtensions
    {
        public static IServiceCollection AddServiceAdapter(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IHttpFactoryService, HttpFactoryService>();
            services.AddSingleton<ICadastrarDadosService, CadastrarDadosService>();

            return services;
        }
    }
}
