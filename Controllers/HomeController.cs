using System.Diagnostics;
using System.Transactions;
using bca_vi_august.Data;
using bca_vi_august.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bca_vi_august.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> AddDummySales()
    {
        var product = _context.Units.FirstOrDefault();
        var product2 = _context.Units.Find(2);

        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var sales = new Sales();
            sales.CustomerName = Guid.NewGuid().ToString();
            sales.TotalAmount = 7800;
            sales.TransactionDate = DateTime.Now;

            var salesDetail1 = new SalesDetail
            {
                Product = product,
                Quantity = 5,
                Rate = 12,
                Discount = 12,
                NetAmount = 48,
            };
            var salesDetail2 = new SalesDetail
            {
                Product = product2,
                Quantity = 10,
                Rate = 15,
                Discount = 30,
                NetAmount = 120,
            };

            sales.Details.Add(salesDetail1);
            sales.Details.Add(salesDetail2);

            _context.Sales.Add(sales);

            await _context.SaveChangesAsync();

            tx.Complete();
        }

        return Content("Item added");
    }

    public async Task<IActionResult> GetSalesData()
    {
        var sales = _context.Sales
            .Include(x => x.Details)
            .ThenInclude(x => x.Product)
            .ToList();

        return Json(sales.Select(x => new
        {
            Id = x.Id,
            CustomerName = x.CustomerName,
            Details = x.Details.Select(y => new
            {
                Id = y.Id,
                ProductName = y.Product.Name,
                NetAmount = y.NetAmount
            })
        }));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}