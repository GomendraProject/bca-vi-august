using bca_vi_august.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace bca_vi_august.Controllers;

public class ProductController : Controller
{
    public IActionResult Add(ProductAddVm vm)
    {
        return View(vm);
    }
}