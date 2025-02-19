using BillingMVC.Web.Models.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BillingMVC.Web.Models
{
    public class BillViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe um nome.")]
        [StringLength(100, MinimumLength = 3,
         ErrorMessage = "Nome da despesa não pode ter menos " +
                        "que 3 caracteres.")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe a moeda.")]
        [DisplayName("Moeda")]
        public CurrencyVM Currency { get; set; }

        [DisplayName("Valor")]
        [DisplayFormat(DataFormatString = "{0:F2}",
            ApplyFormatInEditMode = true)]
        public double Value
        {
            get
            {
                string valorString = ValueString.Replace(',', '.');
                double.TryParse(valorString, 
                    System.Globalization.NumberStyles.Currency, 
                    CultureInfo.InvariantCulture, out var valor);
                return valor;
            }
        }

        [Required(ErrorMessage = "Informe o valor.")]
        public string ValueString { get; set; }

        [Required(ErrorMessage = "Informe a categoria da despesa.")]
        [DisplayName("Tipo")]
        public BillTypeVM Type { get; set; }

        [Required(ErrorMessage = "Informe a origem da despesa.")]
        [StringLength(100, MinimumLength = 2,
         ErrorMessage = "Origem da despesa não pode ter menos " +
                        "que 2 caracteres.")]
        [DisplayName("Origem")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Informe a data da despesa.")]
        [DisplayName("Data da Despesa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }
    }
}