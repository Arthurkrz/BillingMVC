using System.ComponentModel.DataAnnotations;

namespace BillingMVC.Web.Models.Enum
{
    public enum PurchaseMonthVM
    {
        [Display(Name = "Janeiro")]
        January = 1,

        [Display(Name = "Fevereiro")]
        February = 2,

        [Display(Name = "Março")]
        March = 3,

        [Display(Name = "Abril")]
        April = 4,

        [Display(Name = "Maio")]
        May = 5,

        [Display(Name = "Junho")]
        June = 6,

        [Display(Name = "Julho")]
        July = 7,

        [Display(Name = "Agosto")]
        August = 8,

        [Display(Name = "Setembro")]
        September = 9,

        [Display(Name = "Outubro")]
        October = 10,

        [Display(Name = "Novembro")]
        November = 11,

        [Display(Name = "Dezembro")]
        December = 12
    }
}
