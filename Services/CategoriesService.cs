using Mapster;
using Microsoft.AspNetCore.Mvc;
using MyDinDin.Data;
using MyDinDin.Dtos;

namespace MyDinDin.Services;

public class CategoriesService
{
    private readonly MyDinDinContext _contexto;

    public CategoriesService([FromServices] MyDinDinContext contexto)
    {
        _contexto = contexto;
    }
     public List<CategoriesResponseDtos> ToRecoverCategory()
    {
        var category = _contexto.Categories.ToList();

        var categoryResponse = category.Adapt<List<CategoriesResponseDtos>>();

        return categoryResponse;

    }
}
