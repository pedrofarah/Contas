
namespace Contas.Web.Api.Data.Dto
{
    public class PlanoContasDto
    {
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        public bool AceitaLancamentos { get; set; }
        public string? ContaPai { get; set; }
    }
}
