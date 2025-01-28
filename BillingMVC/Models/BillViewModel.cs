using BillingMVC.Core.Enum;
using BillingMVC.Web.Models.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BillingMVC.Web.Models
{
    public class BillViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe um nome.")]
        [StringLength(100, MinimumLength = 3, 
         ErrorMessage = "Nome da conta não pode ter menos que 3 " +
                        "caracteres ou exceder 100 caracteres.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe a moeda.")]
        [DisplayName("Moeda")]
        public CurrencyVM Currency { get; set; }

        [Required(ErrorMessage = "Informe o valor.")]
        [DisplayName("Valor")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Informe a categoria da conta.")]
        [DisplayName("Tipo")]
        public BillTypeVM Type { get; set; }

        [Required(ErrorMessage = "Informe a origem da conta.")]
        [StringLength(100, MinimumLength = 3,
         ErrorMessage = "Origem da conta não pode ter menos que 3 " +
                        "caracteres ou exceder 100 caracteres.")]
        [DisplayName("Origem")]
        public string Source { get; set; }

        [DisplayName("Pago")]
        public CustomBoolean IsPaid { get; set; }

        [Required(ErrorMessage = "Informe a data de vencimento da conta.")]
        [DisplayName("Vencimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Recorrente")]
        public CustomBoolean IsRecurring { get; set; }
    }
}
