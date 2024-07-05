using System.ComponentModel.DataAnnotations;
using TechNation.Enum;

namespace TechNation.Models
{
    public class NotaFiscal
    {
        public int Id { get; set; }
        public string NomePagador { get; set; }
        public string NumeroIdentificacao { get; set; }
        [Display(Name = "Data de Emissão")]
        [DataType(DataType.Date)]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Data de Cobrança")]
        [DataType(DataType.Date)]
        public DateTime? DataCobranca { get; set; }

        [Display(Name = "Data de Pagamento")]
        [DataType(DataType.Date)]
        public DateTime? DataPagamento { get; set; }

        [Display(Name = "Valor da Nota")]
        [DataType(DataType.Currency)]
        public decimal ValorNota { get; set; }
        public string DocumentoNotaFiscal { get; set; }
        public string DocumentoBoletoBancario { get; set; }
        
        public StatusNota Status { get; set; }

    }
}
