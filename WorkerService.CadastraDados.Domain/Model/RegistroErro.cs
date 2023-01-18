namespace WorkerService.CadastraDados.Domain.Model
{
    public class RegistroErro : EntityBase
    {
        public int CodErr { get; set; }
        public string MsgErr { get; set; }
        public string Metodo { get; set; }
        public string Entrada { get; set; }
        public string Exception { get; set; }
        public DateTime DataHorAtualizacao { get; set; }
    }
}
