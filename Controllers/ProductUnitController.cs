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

    public IActionResult Edit(int id)
    {
        // To edit
        // 1. Identifier to identify data : ID
        // 2. Validate that the item exists
        // 3. Get the data and display the data in the form
        try
        {
            var item = productUnits.Where(x => x.Id == id)
                .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item not found");
            }
            // ViewModel
            var vm = new UnitEditVm();
            vm.Name = item.Name;
            vm.Description = item.Name;
            return View(vm);
        }
        catch (Exception e)
        {
            // Send error message
            return RedirectToAction("Index");
        }
    }
    
    [HttpPost]
    public IActionResult Edit(int id, UnitEditVm vm)
    {
        try
        {
            var item = productUnits.Where(x => x.Id == id)
                .FirstOrDefault();
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            // Update data in database. Updating data locally for now
            // The changes wont show up in view
            item.Name = vm.Name;
            // Save Changes
            // Send Success Message
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            // Send error message
            return RedirectToAction("Index");
        }
    }

}