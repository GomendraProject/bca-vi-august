using System.ComponentModel.DataAnnotations.Schema;

namespace bca_vi_august.Models;

[Table("inv_sales_details")]
public class SalesDetail
{
    public long Id { get; set; }
    
    public virtual Sales Sale { get; set; }
    
    public long SaleId { get; set; }
    
    public virtual ProductUnit Product { get; set; }
    public long ProductId { get; set; }
    
    public long Quantity { get; set; }
    
    public decimal Rate { get; set; }
    
    public decimal Discount { get; set; }
    
    public decimal NetAmount { get; set; }
}