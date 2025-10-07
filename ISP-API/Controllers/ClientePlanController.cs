using ISP_API.Data;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ISP_API.Controllers;

[ApiController]
[Route("api/clientesDetalles")]
public class ClientePlanController : ControllerBase
{
    private readonly IPlanClienteService _planClienteService;
    private readonly AppDbContext _dbContext;
    public ClientePlanController(IPlanClienteService planClienteService, AppDbContext dbContext)
    {
        _planClienteService = planClienteService;
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllClientePlanes() 
    {
        var response = await _planClienteService.GetAllClientePlanesAsync();
        if (!response.Status)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    
    [HttpGet("/{clienteId}")]
    public async Task<IActionResult> GetClientePlanByClienteId(Guid clienteId) 
    {
        var response = await _planClienteService.GetClientePlanByClienteIdAsync(clienteId);
        if (!response.Status)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}