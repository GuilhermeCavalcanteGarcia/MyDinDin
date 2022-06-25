using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDinDin.Models;

public class Goal
{
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "float(53)")]
    public decimal ValueGoal { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Descripition { get; set; }

    // PROPRIEDADE DE NAVEGAÇÃO 
    public User User { get; set; }


    // PROPPRIEDADE CHAVE ESTRANGEIRA
    // (NOMEADA USANDO CONVEÇÃO - (CLASSEID))
    [Required]
    public int UserId { get; set; }
}
