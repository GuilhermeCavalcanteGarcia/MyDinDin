using Microsoft.AspNetCore.Mvc;
using MyDinDin.Dtos;
using MyDinDin.Services;

namespace MyDinDin.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesService _categoryService;
    public CategoriesController([FromServices] CategoriesService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public ActionResult<List<CategoriesResponseDtos>> GetCategory()
    {
        try
        {
            var category = _categoryService.ToRecoverCategory();

            return Ok(category);
        }
        catch(Exception)
        {
            return NotFound();
        }
    }
}
