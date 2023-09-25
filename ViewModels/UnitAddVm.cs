using System.ComponentModel.DataAnnotations;
using bca_vi_august.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bca_vi_august.ViewModels;

public class UnitAddVm
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    // For Data binding
    public long CategoryId { get; set; }

    // To Show Select List
    public List<Category> Categories;
    
    // Use Select List

    public SelectList CategoryOptionSelectList()
    {
        return new SelectList(
            Categories, // List of items
            nameof(Category.Id), // Which Property to use for Value 
            nameof(Category.Name), // Which property to use for display
            CategoryId // Selected value
        );
    }
}