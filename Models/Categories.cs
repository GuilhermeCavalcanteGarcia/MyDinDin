using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDinDin.Models;

public class Categories
{
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string NameCategory { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Descripition { get; set; }

    // PROPPRIEDADE DE NAVEGAÇÃO 
    public List<Expenses> Expenses { get; set; }


}
