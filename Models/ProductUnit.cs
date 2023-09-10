using System.ComponentModel.DataAnnotations.Schema;

namespace bca_vi_august.Models;

[Table("inv_unit")]
public class ProductUnit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    // Add a column    
    public string Description { get; set; }
}