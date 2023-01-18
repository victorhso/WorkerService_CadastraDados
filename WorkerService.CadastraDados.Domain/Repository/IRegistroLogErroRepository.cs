namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface IRegistroLogErroRepository
    {
        public void RegistrarErro(int codErr, string msgErr, string metodo, string entrada = null, Exception ex = null);
    }
}