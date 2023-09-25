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
    
    // Adding column and relation
    public long CategoryId { get; set; }
    
    public virtual Category Category { get; set; }
   
    // public long OldCategoryInfo { get; set; }
    //
    // [ForeignKey("OldCategoryInfo")]
    // public virtual Category OldCat { get; set; }
    
    
    // Format: public virtual TableModelName {ColumnName} {get; set; }
    // Column Format: public long {ColumnName}Id {get;set;}
}