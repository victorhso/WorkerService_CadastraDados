using WorkerService.CadastraDados.Domain.Model;

namespace WorkerService.CadastraDados.Domain.Dtos
{
    public class DadosCadastroDto
    {
        public Pessoa Pessoa { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
    }
}
