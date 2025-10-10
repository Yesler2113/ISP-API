using ISP_API.Dtos.EquipoDTOs;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISP_API.Controllers;

[Authorize]
[ApiController]
[Route("api/equipos")]
public class EquipoController : ControllerBase
{
    private readonly IEquipoService _equipoService;
    public EquipoController(IEquipoService equipoService)
    {
        _equipoService = equipoService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEquipo([FromBody] EquipoCreateDto model)
    {
        var response = await _equipoService.CreateEquipoAsync(model);
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllEquipos()
    {
        var response = await _equipoService.GetAllEquiposAsync();
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEquipoById(Guid id)
    {
        var response = await _equipoService.GetEquipoByIdAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipo(Guid id, [FromBody] EquipoDto model)
    {
        model.Id = id;
        var response = await _equipoService.UpdateEquipoAsync(model);
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipo(Guid id)
    {
        var response = await _equipoService.DeleteEquipoAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }
}