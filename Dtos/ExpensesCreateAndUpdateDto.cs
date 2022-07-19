namespace MyDinDin.Dtos;

public class ExpensesCreateAndUpdateDto
{
    public int UserId { get; set; } = 1;
    public int CategoriesId { get; set; } 
    public string Descripition { get; set; }
    public double Value { get; set; }
}
