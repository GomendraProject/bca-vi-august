using System.ComponentModel.DataAnnotations.Schema;

namespace bca_vi_august.Models;

[Table("inv_category")]
public class Category
{
    // -> Id
    // -> Name
    // -> Description

    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}