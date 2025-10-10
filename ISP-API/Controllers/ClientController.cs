using ISP_API.Data;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Entities;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISP_API.Controllers;
[Authorize]
[ApiController]
[Route("api/clientes")]
public class ClientController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IClienteService _clienteService;
    public ClientController(IClienteService clienteService, AppDbContext context)
    {
        _clienteService = clienteService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { mensaje = "Datos inválidos", errores = ModelState });

        var cliente = new ClienteEntity
        {
            CodigoCliente = dto.CodigoCliente,
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Identidad = dto.Identidad,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono,
            FechaInicio = dto.FechaInicio,
            CostoInstalacion = dto.CostoInstalacion
        };

        // Primero agregamos el cliente a la base de datos para generar el ID
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // ✅ Si el cliente tiene planes contratados
        if (dto.PlanesContratadosIds != null && dto.PlanesContratadosIds.Any())
        {
            var planes = dto.PlanesContratadosIds.Select(planId => new ClientePlanEntity
            {
                Cliente = cliente,  // ✅ Ya tenemos el ID del cliente
                PlanId = planId
            }).ToList();

            _context.Planes.AddRange(planes);
        }

        // ✅ Si el cliente tiene equipos asignados
        if (dto.EquiposIds != null && dto.EquiposIds.Any())
        {
            var equipos = dto.EquiposIds.Select(equipoId => new EquipoClienteEntity
            {
                ClienteId = cliente.Id,
                EquipoId = equipoId
            }).ToList();

            _context.EquipoClientes.AddRange(equipos);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Cliente creado correctamente",
            cliente.Id
        });
    }



    [HttpGet("todos")]
    public async Task<IActionResult> GetAllClientes()
    {
        var response = await _clienteService.GetAllClientesAsync();
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClienteById(Guid id)
    {
        var response = await _clienteService.GetClienteByIdAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        return NotFound(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarCliente(Guid id, [FromBody] ClienteUpdateDto dto)
    {
        var result = await _clienteService.EditarClienteAsync(id, dto);

        if (!result.Status)
            return StatusCode(result.StatusCode, result.Message);

        return Ok(result);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(Guid id)
    {
        var response = await _clienteService.DeleteClienteAsync(id);
        if (response.Status)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
}
    
    
