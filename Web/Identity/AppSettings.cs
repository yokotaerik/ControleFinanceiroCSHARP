namespace ControleFinanceiro.Web.Identity
{
    public class AppSettings
    {
        public string Secret { get; set; } //Segredo
        public int ExpirationInHours { get; set; } //Expiração em Horas
        public string Issuer { get; set; } // Emissor
        public string Audience { get; set; } //Valido Em
    }
}
