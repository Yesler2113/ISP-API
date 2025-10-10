using ISP_API.Data;
using ISP_API.Dtos;
using ISP_API.Dtos.PagoDTOs;
using ISP_API.Entities;
using ISP_API.Services.Entities;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace ISP_API.Services;

public class PagoService : IPagoService
{
    private readonly AppDbContext _context;
    public PagoService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseDto<PagoEntity>> RegistrarPagoAsync(PagoCreateDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null)
        {
            return new ResponseDto<PagoEntity>
            {
                Status = false,
                StatusCode = 400,
                Message = "Cliente no encontrado.",
                Data = null
            };
        }
        var pago = new PagoEntity
        {
            Id = Guid.NewGuid(),
            ClienteId = dto.ClienteId,
            FechaPago = DateTime.UtcNow,
            MontoPagado = dto.MontoPagado,
            MontoTotal = dto.MontoTotal,
            SaldoPendiente = dto.SaldoPendiente,
            EsPagoCompleto = dto.EsPagoCompleto,
            Detalles = dto.Detalles.Select(d => new PagoDetalleEntity
            {
                Id = Guid.NewGuid(),
                PlanId = d.PlanId,
                Monto = d.Monto
            }).ToList()
        };
        // Actualizar saldo del cliente
        if (cliente != null)
        {
            if (dto.EsPagoCompleto)
            {
                cliente.SaldoActual = 0;
                cliente.ProximoPago = DateTime.UtcNow.AddMonths(1); // Próximo ciclo
            }
            else
            {
                cliente.SaldoActual = dto.SaldoPendiente;
                // No cambia el proximo pago si no está al día
            }

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }
        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();
        
        //generar el recibo de pago en pdf
        byte[] pdfBytes = await GenerarReciboPagoAsync(pago.Id);

        return new ResponseDto<PagoEntity>
        {
            Status = true,
            StatusCode = 201,
            Message = "Pago registrado correctamente.",
            Data = pago
        };
    }
    
    //generar el recibo de pago en formato pdf
    public async Task<byte[]> GenerarReciboPagoAsync(Guid pagoId)
    {
        var pago = await _context.Pagos
            .Include(p => p.Cliente)
            .Include(p => p.Detalles!)
                .ThenInclude(d => d.Plan)
            .FirstOrDefaultAsync(p => p.Id == pagoId);

        if (pago == null)
            throw new Exception("Pago no encontrado");

        var documento = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                // ======= ENCABEZADO =======
                page.Header().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingBottom(10).Row(row =>
                {
                    row.RelativeColumn(2).Column(col =>
                    {
                        col.Item().Text("StellarSoft ISP")
                            .Bold().FontSize(20).FontColor(Colors.Blue.Medium);
                        col.Item().Text("Factura / Recibo de Pago").FontSize(14);
                    });

                    //row.ConstantColumn(80).Height(80).Image("https://i.ibb.co/ZxH2wV8/stellarsoft-logo.png"); // Cambia por tu logo real
                });

                // ======= CUERPO =======
                page.Content().PaddingVertical(20).Column(col =>
                {
                    col.Item().Background(Colors.Grey.Lighten5).Padding(10).Column(info =>
                    {
                        info.Item().Text("Datos del Cliente").Bold().FontSize(13);
                        info.Item().Text($"{pago.Cliente!.Nombre} {pago.Cliente.Apellido}");
                        info.Item().Text($"Dirección: {pago.Cliente.Direccion}");
                        info.Item().Text($"Teléfono: {pago.Cliente.Telefono}");
                        info.Item().Text($"Código Cliente: {pago.Cliente.CodigoCliente}");
                    });

                    col.Item().PaddingTop(10).Background(Colors.White).Padding(10).Column(info =>
                    {
                        info.Item().Text("Información del Pago").Bold().FontSize(13);
                        info.Item().Text($"Fecha de pago: {pago.FechaPago:dd/MM/yyyy}");
                        info.Item().Text(pago.EsPagoCompleto ? "Pago completo" : "Pago parcial");
                    });

                    col.Item().PaddingTop(20).Text("Planes pagados").Bold().FontSize(13);

                    // ======= TABLA DETALLES =======
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                        });

                        // Header
                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Plan").FontColor(Colors.White);
                            header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Monto").FontColor(Colors.White);
                        });

                        // Rows
                        foreach (var detalle in pago.Detalles!)
                        {
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                                .Text(detalle.Plan!.Nombre);
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                                .Text($"{detalle.Monto:C}");
                        }
                    });

                    // ======= TOTALES =======
                    col.Item().PaddingTop(15).Row(row =>
                    {
                        row.RelativeColumn();
                        row.ConstantColumn(200).Background(Colors.Grey.Lighten4).Padding(10).Column(tot =>
                        {
                            tot.Item().Row(r =>
                            {
                                r.RelativeColumn().Text("Monto Total:").Bold();
                                r.ConstantColumn(80).AlignRight().Text($"{pago.MontoTotal:C}");
                            });
                            tot.Item().Row(r =>
                            {
                                r.RelativeColumn().Text("Monto Pagado:");
                                r.ConstantColumn(80).AlignRight().Text($"{pago.MontoPagado:C}");
                            });
                            tot.Item().Row(r =>
                            {
                                r.RelativeColumn().Text("Saldo Pendiente:");
                                r.ConstantColumn(80).AlignRight().Text($"{pago.SaldoPendiente:C}");
                            });
                        });
                    });
                });

                // ======= PIE DE PÁGINA =======
                page.Footer().AlignCenter().PaddingTop(10).Text("Gracias por su pago. Factura sin Valor Fiscal - StellarSoft ISP © 2025")
                    .FontSize(10)
                    .Italic()
                    .FontColor(Colors.Grey.Darken1);
            });
        });

        return documento.GeneratePdf();
    }
    
    
    //obtener resumen
    public async Task<object> GetResumenDashboardAsync()
    {
        var hoy = DateTime.UtcNow;

        var clientesTotales = await _context.Clientes.CountAsync();

        var clientesPendientes = await _context.Clientes
            .CountAsync(c => c.SaldoActual > 0);
        //solo si estan a 5 dias de que se cumpla el mes de pago
        var clientesProximosAPagar = await _context.Clientes
            .CountAsync(c => c.ProximoPago <= hoy.AddDays(5) && c.SaldoActual == 0);
        
        
       

        var clientesAlDia = clientesTotales - clientesPendientes;

        return new
        {
            TotalClientes = clientesTotales,
            Pendientes = clientesPendientes,
            ProximosAPagar = clientesProximosAPagar,
            AlDia = clientesAlDia
        };
    }
    
    //actualizar saldos automaticamente
    public async Task ActualizarSaldosClientesAsync()
    {
        var hoy = DateTime.UtcNow;
        var clientes = await _context.Clientes
            .Where(c => c.ProximoPago <= hoy.AddDays(3) && c.SaldoActual == 0)
            .ToListAsync();

        foreach (var cliente in clientes)
        {
            // Reactivar cobro mensual
            var totalPlanes = await _context.Planes
                .Include(cp => cp.Plan)
                .Where(cp => cp.ClienteId == cliente.Id)
                .SumAsync(cp => cp.Plan!.Precio);

            cliente.SaldoActual = totalPlanes;
            cliente.FechaPago = cliente.ProximoPago;
        }

        await _context.SaveChangesAsync();
    }
}