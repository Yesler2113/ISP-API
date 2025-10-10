using ISP_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace ISP_API.Controllers;

[ApiController]
[Route("api/prueba")]
public class PruebaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    public PruebaController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        
    }
    //agregar un metodo que retorne un string
    [HttpGet("api/prueba")]
    public IActionResult Get()
    {
        return Ok("Hola desde el controlador de prueba");
        
    }
        
}