using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDinDin.Models;


public class User
{
    
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Nome { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Email { get; set; }
    [Required]
    [Column(TypeName = "varchar(8)")]
    public string Senha { get; set; }
    [Required]
    [Column(TypeName = "varchar(8)")]
    public string Password_Salt { get; set; }

    // PROPRIEDADES DE NAVEGAÇÃO 
    public List<Income> Income { get; set; }
    public List<Expenses> Expenses { get; set; }
    public List<Goal> Goal { get; set; }


}
