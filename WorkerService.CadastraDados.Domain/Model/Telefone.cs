namespace WorkerService.CadastraDados.Domain.Model
{
    public class Telefone : EntityBase
    {
        public int Ddi { get; set; }
        public int Ddd { get; set; }
        public string NumeroCelular { get; set; }
        public string NumeroTelefone { get; set; }
        public int IdPessoa { get; set; }
    }
}
