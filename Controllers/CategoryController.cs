using System.Transactions;
using bca_vi_august.Data;
using bca_vi_august.Models;
using bca_vi_august.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bca_vi_august.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryCreateVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // If user data is not valid
                // Validation failed
                return View(vm);
            }

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Create a model object
                var category = new Category();
                category.Name = vm.Name;
                category.Description = vm.Description;

                // Mark to be added to database
                _context.Categories.Add(category);
                
                // Send changes to database
                await _context.SaveChangesAsync();
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
}