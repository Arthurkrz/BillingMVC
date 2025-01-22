using System.ComponentModel.DataAnnotations;

namespace BillingMVC.Web.Models.Enum
{
    public enum BillTypeVM
    {
        [Display(Name = "Selecione")]
        Select,

        [Display(Name = "Alimentação")]
        Food,

        [Display(Name = "Transporte")]
        Transport,

        [Display(Name = "Residência")]
        House,

        [Display(Name = "Lazer")]
        Fun,

        [Display(Name = "Serviços")]
        Services,

        [Display(Name = "Dívidas")]
        Debts,

        [Display(Name = "Empréstimos")]
        Loan
    }
}
