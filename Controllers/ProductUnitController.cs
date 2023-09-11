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
            ).ToListAsync();
        return View(vm);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(UnitAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var unit = new ProductUnit();
                unit.Name = vm.Name;
                unit.Description = vm.Description;
                
                // Mark this object to be inserted
                _context.Units.Add(unit);
                
                // Send changes to database
                await _context.SaveChangesAsync();
                // Complete the transaction
                tx.Complete();
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
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Update data in database. Updating data locally for now
                // The changes wont show up in view
                item.Name = vm.Name;
                item.Description = vm.Description;

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