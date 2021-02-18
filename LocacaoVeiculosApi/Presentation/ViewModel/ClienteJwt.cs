namespace LocacaoVeiculosApi.Presentation.ViewModel
{
    public record ClienteJwt
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string Cpf { get; set; }
        public string tipoUsuario { get; set; }
        public string Token { get; set; }
    }
}