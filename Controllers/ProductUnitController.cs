using bca_vi_august.Models;
using bca_vi_august.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bca_vi_august.Controllers;

public class ProductUnitController : Controller
{
    
    List<ProductUnit> productUnits = new List<ProductUnit>()
    {
        new ProductUnit()
        {
            Id = 1,
            Name = "Kilo"
        },
        new ProductUnit()
        {
            Id = 2,
            Name = "Piece"
        },
        new ProductUnit()
        {
            Id = 3,
            Name = "Dozen"
        },
        new ProductUnit()
        {
            Id = 4,
            Name = "Pound"
        }
    };

    
    public IActionResult Index(UnitIndexVm vm)
    {
        // Use dummy data for now
        // Get data from database later
        vm.Data = productUnits
            .Where(x =>
                string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
            ).ToList();
        return View(vm);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(UnitAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // Send success message
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            // Send error message
            return RedirectToAction("Index");
        }
    }
}