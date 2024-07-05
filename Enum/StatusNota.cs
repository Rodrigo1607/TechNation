using System.ComponentModel.DataAnnotations;

namespace TechNation.Enum
{
    public enum StatusNota
    {
        [Display(Name = "Emitida")]
        Emitida,

        [Display(Name = "Cobrança realizada")]
        CobrancaRealizada,

        [Display(Name = "Pagamento em atraso")]
        PagamentoEmAtraso,

        [Display(Name = "Pagamento realizado")]
        PagamentoRealizado
    }
}
