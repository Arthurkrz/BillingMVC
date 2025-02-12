using System.ComponentModel.DataAnnotations;

namespace BillingMVC.Core.Enum
{
    public enum Currency
    {
        [Display(Name = "Euro")]
        Euro,

        [Display(Name = "Real")]
        Real
    }
}
