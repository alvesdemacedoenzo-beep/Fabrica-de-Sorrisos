using FabricaDeSorrisos.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaixasEtariasController : ControllerBase
{
    private readonly ICatalogLookupService _service;

    public FaixasEtariasController(ICatalogLookupService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetFaixasEtariasAsync());
    }
}
