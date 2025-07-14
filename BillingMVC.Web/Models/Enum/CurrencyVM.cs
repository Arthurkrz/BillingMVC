using System.ComponentModel.DataAnnotations;

namespace BillingMVC.Web.Models.Enum
{
    public enum CurrencyVM
    {
        [Display(Name = "Euro")]
        Euro,

        [Display(Name = "Real")]
        Real
    }
}
