
namespace Contas.Web.Api.Data.Entities
{
    public class PlanoContas
    {
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public bool AceitaLancamentos { get; set; }
        public string? ContaPai { get; set; }
    }
}
