using ISP_API.Data;
using ISP_API.Dtos.PagoDTOs;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISP_API.Controllers;
[ApiController]
[Route("api/pagos")]
public class PagosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPagoService _pagoService;
    public PagosController(IPagoService pagoService, AppDbContext context)
    {
        _pagoService = pagoService;
        _context = context;
    }
    
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarPago([FromBody] PagoCreateDto dto)
    {
        var result = await _pagoService.RegistrarPagoAsync(dto);
        if (!result.Status) return BadRequest(result);
        var pdfBytes = await _pagoService.GenerarReciboPagoAsync(result.Data!.Id);
        return File(pdfBytes, "application/pdf", $"Recibo_{result.Data.Id}.pdf");
    }

    [HttpGet("recibo/{pagoId}")]
    public async Task<IActionResult> GenerarRecibo(Guid pagoId)
    {
        var pdf = await _pagoService.GenerarReciboPagoAsync(pagoId);
        return File(pdf, "application/pdf", $"Recibo_{pagoId}.pdf");
    }

    [HttpGet("dashboard/resumen")]
    public async Task<IActionResult> GetResumenDashboard()
    {
        var resumen = await _pagoService.GetResumenDashboardAsync();
        return Ok(resumen);
    }
    
    [HttpGet("preparar/{clienteId}")]
    public async Task<IActionResult> PrepararPago(Guid clienteId)
    {
        var cliente = await _context.Clientes
            .Include(c => c.PlanesContratados!)
            .ThenInclude(cp => cp.Plan)
            .FirstOrDefaultAsync(c => c.Id == clienteId);

        if (cliente == null)
            return NotFound(new { message = "Cliente no encontrado." });

        var totalPlanes = cliente.PlanesContratados!.Sum(p => p.Plan!.Precio);

        var dto = new
        {
            ClienteId = cliente.Id,
            ClienteNombre = $"{cliente.Nombre} {cliente.Apellido}",
            ClienteCodigo = cliente.CodigoCliente,
            FechaPago = DateTime.UtcNow,
            MontoTotal = totalPlanes,
            MontoPagado = 0,
            SaldoPendiente = totalPlanes,
            EsPagoCompleto = false,
            Detalles = cliente.PlanesContratados.Select(p => new
            {
                PlanId = p.Plan!.Id,
                PlanNombre = p.Plan.Nombre,
                Monto = p.Plan.Precio
            }).ToList()
        };

        return Ok(dto);
    }
    
    [HttpGet("actualizar-saldos")]
    public async Task<IActionResult> ActualizarSaldos()
    {
        await _pagoService.ActualizarSaldosClientesAsync();
        return Ok(new { message = "Saldos actualizados correctamente" });
    }

}