using Microsoft.EntityFrameworkCore;
using WorkerService.CadastraDados;
using WorkerService.CadastraDados.Application;
using WorkerService.CadastraDados.Application.HttpFactory;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Repository;
using WorkerService.CadastraDados.Domain.Services;
using WorkerService.CadastraDados.Repository;

namespace MB.WorkerService.GRP.PagamentoAutorizacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration Configuration = hostContext.Configuration;

                    //services.AddHttpClient();

                    string cnn = Configuration.GetConnectionString("SqlConnectionString");
                    services.AddDbContext<Context>(options => options.UseSqlServer(cnn), ServiceLifetime.Transient, ServiceLifetime.Transient);
                    services.Configure<RabbitMQConfiguration>(Configuration.GetSection("RabbitMq"));
                    RabbitMQConfiguration rabbitMqConfiguration = Configuration.GetSection("RabbitMq").Get<RabbitMQConfiguration>();
                    services.AddSingleton<RabbitMQConfiguration>(rabbitMqConfiguration);

                    //services.AddDbRepositoryAdapter();
                    //services.AddServiceAdapter();

                    services.AddSingleton<IRabbitMQRepository, RabbitMQRepository>();
                    services.AddSingleton<IPessoaRepository, PessoaRepository>();
                    services.AddSingleton<ITelefoneRepository, TelefoneRepository>();
                    services.AddSingleton<IEnderecoRepository, EnderecoRepository>();
                    services.AddSingleton<IRegistroLogErroRepository, RegistroLogErroRepository>();

                    services.AddSingleton<IHttpFactoryService, HttpFactoryService>();
                    services.AddSingleton<ICadastrarDadosService, CadastrarDadosService>();

                    services.AddHostedService<Worker>();

                    services.Configure<ConnStrings>(Configuration.GetSection("ConnectionStrings"));
                    ConnStrings connStrings = Configuration.GetSection("ConnectionStrings").Get<ConnStrings>();
                    services.AddSingleton<ConnStrings>(connStrings);
                });
    }
}