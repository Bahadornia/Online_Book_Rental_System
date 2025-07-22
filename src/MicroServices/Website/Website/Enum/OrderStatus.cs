using System.ComponentModel.DataAnnotations;
namespace Website.Enum;

public enum OrderStatus
{
    None = 0,
    [Display(Name = "Approved", ResourceType = typeof(Resources.resources))]
    Approved = 10,
    [Display(Name = "Rejected", ResourceType = typeof(Resources.resources))]
    Rejected = 20,
    [Display(Name = "OverDue", ResourceType = typeof(Resources.resources))]
    OverDue = 30,
    [Display(Name = "Extended", ResourceType = typeof(Resources.resources))]
    Extended = 40
}
