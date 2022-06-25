using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDinDin.Models;

public class Expenses
{
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Descripition { get; set; }
    [Required]
    public double Value { get; set; }
    [Required]
    public DateTime Time { get; set; }

    // PROPRIEDADE DE NAVEGAÇÃO 
    public Categories Categorie { get; set; }

    // PROPPRIEDADE DE CHAVE ESTRANGEIRA 
    // NOMEADA USANDO CONVENÇÃO - CLASSEID
    [Required]
    public int CategoriesId { get; set; }
}
