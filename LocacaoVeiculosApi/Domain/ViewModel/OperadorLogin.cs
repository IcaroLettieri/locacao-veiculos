namespace LocacaoVeiculosApi.Domain.ViewModel
{
    public record OperadorLogin
    {
        public string Matricula {get; set;}
        public string Senha {get; set;}
    }
}