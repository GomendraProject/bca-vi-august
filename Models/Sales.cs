using System.ComponentModel.DataAnnotations.Schema;

namespace bca_vi_august.Models;

[Table("inv_sales")]
public class Sales
{
    public long Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public string CustomerName { get; set; }
    
    public decimal TotalAmount { get; set; }

    // One to many relations
    public virtual List<SalesDetail> Details { get; set; } = new List<SalesDetail>();
    
    // One To One
    // Only required in one side
    
    // One to many
    // Required in both
}