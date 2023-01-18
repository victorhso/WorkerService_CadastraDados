using Newtonsoft.Json;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Enums;
using WorkerService.CadastraDados.Domain.Model;
using WorkerService.CadastraDados.Domain.Repository;
using WorkerService.CadastraDados.Domain.Services;

namespace WorkerService.CadastraDados.Application
{
    public class CadastrarDadosService : ICadastrarDadosService
    {
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IHttpFactoryService _httpFactoryService;
        private readonly IRegistroLogErroRepository _registroLogErroRepository;

        public CadastrarDadosService(ITelefoneRepository telefoneRepository, IPessoaRepository pessoaRepository, IEnderecoRepository enderecoRepository, IHttpFactoryService httpFactoryService, IRegistroLogErroRepository registroLogErroRepository)
        {
            _telefoneRepository = telefoneRepository;
            _pessoaRepository = pessoaRepository;
            _enderecoRepository = enderecoRepository;
            _httpFactoryService = httpFactoryService;
            _registroLogErroRepository = registroLogErroRepository;
        }

        public async Task CadastrarDados(DadosCadastroDto dadosCadastroDto)
        {
            try
            {
                _registroLogErroRepository.RegistrarErro((int)CodigoErro.Sucesso, $"Inicio do Serviço às {DateTime.Now}", "CadastrarDados", JsonConvert.SerializeObject(dadosCadastroDto), null);

                _pessoaRepository.InsertPessoa(dadosCadastroDto.Pessoa);

                dadosCadastroDto.Telefone.IdPessoa = dadosCadastroDto.Pessoa.ID;
                _telefoneRepository.InsertTelefone(dadosCadastroDto.Telefone);

                var consultaCep = await _httpFactoryService.BuscarEnderecoPorCep(dadosCadastroDto.Endereco.Cep);

                if (consultaCep != null)
                {
                    dadosCadastroDto.Endereco = PreencheEndereco(dadosCadastroDto.Endereco, consultaCep, dadosCadastroDto.Pessoa.ID);
                    _enderecoRepository.InsertEndereco(dadosCadastroDto.Endereco);
                }
            }
            catch (Exception ex)
            {
                _registroLogErroRepository.RegistrarErro((int)CodigoErro.Falha, ex.Message, "CadastrarDados", JsonConvert.SerializeObject(dadosCadastroDto), ex);
            }
        }

        private Endereco PreencheEndereco(Endereco endereco, ResponseBuscaEnderecoPorCepDto consultaCep, int idPessoa)
        {
            endereco.Rua = consultaCep.logradouro;
            endereco.Numero = 0;
            endereco.Bairro = consultaCep.bairro;
            endereco.Cidade = consultaCep.localidade;
            endereco.UF = consultaCep.uf;
            endereco.Pais = "Brasil";
            endereco.IdPessoa = idPessoa;

            return endereco;
        }
    }
}
