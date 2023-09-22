using bca_vi_august.Models;

namespace bca_vi_august.ViewModels;

public class UnitIndexVm
{
    // For Retrieving user input
    public string Name { get; set; }

    // Sending data to view
    public List<ProductUnit> Data;
    
    // Vm
    public List<ProductUnitDisplayVm> DisplayData;
}