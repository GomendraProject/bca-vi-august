using System.Transactions;
using bca_vi_august.Data;
using bca_vi_august.Models;
using bca_vi_august.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bca_vi_august.Controllers;

public class ProductUnitController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductUnitController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index(UnitIndexVm vm)
    {
        vm.Data = await _context.Units
            .Where(x =>
                string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
            )
            .Include(x => x.Category) // To Get Category data automatically
            .ToListAsync();
        
        vm.DisplayData = await _context.Units
            .Where(x =>
                string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
            )
            .Select(product => new ProductUnitDisplayVm()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                CategoryId = product.Category.Id
            })
            .ToListAsync();

        var items = await _context.Categories
            .Where(category => category.Id == 45)
            .Where(category => category.Name.StartsWith("X_") || category.Name.StartsWith("Y_"))
            .OrderBy(x => x.Name)
            .ToListAsync();
        
        // Usually
        // 1. Find by id
        // 2. Search
        
        return View(vm);
    }

    public async Task<IActionResult> Add()
    {
        var vm = new UnitAddVm();
        vm.Categories = await _context.Categories.ToListAsync();
      //  await ConfigureAddViewVm(vm);
        return View(vm);
    }

    public async Task ConfigureAddViewVm(UnitAddVm vm)
    {
        vm.Categories = await _context.Categories.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Add(UnitAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                // await ConfigureAddViewVm(vm);
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var unit = new ProductUnit();
                unit.Name = vm.Name;
                unit.Description = vm.Description;
                
                // Option1: Use ID directly
                unit.CategoryId = vm.CategoryId;
                
                // Option 2: Use object relations

                // var category = await _context.Categories.Where(x => x.Id == vm.CategoryId)
                //     .FirstOrDefaultAsync();
                //
                // unit.Category = category;
                
                // Mark this object to be inserted
                _context.Units.Add(unit);
                
                // Send changes to database
                await _context.SaveChangesAsync();
                // Complete the transaction
                tx.Complete();
            }

            // Send success message
            return RedirectToAction("Index");

            return RedirectToAction("Index", "Home");

            return Redirect("/product/index");
        }
        catch (Exception e)
        {
            // Send error message
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        // To edit
        // 1. Identifier to identify data : ID
        // 2. Validate that the item exists
        // 3. Get the data and display the data in the form
        try
        {
            var item = await _context.Units.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            // ViewModel
            var vm = new UnitEditVm();
            vm.Name = item.Name;
            vm.Description = item.Description;
            vm.CategoryId = item.CategoryId;
            
            vm.Categories = await _context.Categories.ToListAsync();
            
            return View(vm);
        }
        catch (Exception e)
        {
            // Send error message
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UnitEditVm vm)
    {
        try
        {
            var item = await _context.Units.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Update data in database. Updating data locally for now
                // The changes wont show up in view
                item.Name = vm.Name;
                item.Description = vm.Description;
                item.CategoryId = vm.CategoryId;

                await _context.SaveChangesAsync();
                
                tx.Complete();
            }
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