using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Enums;
using WorkerService.CadastraDados.Domain.Model;
using WorkerService.CadastraDados.Domain.Repository;
using WorkerService.CadastraDados.Domain.Services;

namespace WorkerService.CadastraDados
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQRepository _rabbitMQRepository;
        private readonly ICadastrarDadosService _cadastrarDadosService;
        private readonly RabbitMQConfiguration _rabbitMQConfiguration;
        private readonly IRegistroLogErroRepository _registroLogErroRepository;

        public Worker(ILogger<Worker> logger, IRabbitMQRepository rabbitMQRepository, ICadastrarDadosService cadastrarDadosService, RabbitMQConfiguration rabbitMQConfiguration, IRegistroLogErroRepository registroLogErroRepository)
        {
            _logger = logger;
            _rabbitMQRepository = rabbitMQRepository;
            _cadastrarDadosService = cadastrarDadosService;
            _rabbitMQConfiguration = rabbitMQConfiguration;
            _registroLogErroRepository = registroLogErroRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                string requestExemple = JsonConvert.SerializeObject(PreencheObjTest());
                await Execute();
                await Task.Delay(3600000, stoppingToken);
            }
        }

        private async Task Execute()
        {
            EventHandler<BasicDeliverEventArgs> consumeEvent = async (model, ea) =>
            {
                try
                {
                    byte[] body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);

                    if (ea.RoutingKey.Equals(_rabbitMQConfiguration.CADASTRAR_DADOS))
                    {
                        DadosCadastroDto dadosCadastroDto = JsonConvert.DeserializeObject<DadosCadastroDto>(message);
                        await _cadastrarDadosService.CadastrarDados(dadosCadastroDto);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message} ### {ex.StackTrace}");
                    _registroLogErroRepository.RegistrarErro((int)CodigoErro.Falha, ex.Message, "CadastrarDados", null, ex);
                }
            };
            _rabbitMQRepository.ReadFromQueue(consumeEvent, _rabbitMQConfiguration.CADASTRAR_DADOS);
        }

        private DadosCadastroDto PreencheObjTest()
        {
            Pessoa pessoa = new Pessoa()
            {
                Nome = "",
                Cpf = "",
                DataNascimento = DateTime.Now,
                Email = "",
                Sexo = ""
            };

            Telefone telefone = new Telefone()
            {
                Ddi = 55,
                Ddd = 31,
                NumeroCelular = "",
                NumeroTelefone = "",
                IdPessoa = 1
            };

            Endereco endereco = new Endereco()
            {
                Rua = "",
                Numero = 0,
                Bairro = "",
                Cidade = "",
                Cep = "",
                Pais = "",
                UF = ""
            };

            DadosCadastroDto dadosCadastroDto = new DadosCadastroDto()
            {
                Pessoa = pessoa,
                Endereco = endereco,
                Telefone = telefone
            };

            return dadosCadastroDto;
        }
    }
}