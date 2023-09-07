using System.ComponentModel.DataAnnotations;

namespace bca_vi_august.ViewModels;

public class UnitAddVm
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}