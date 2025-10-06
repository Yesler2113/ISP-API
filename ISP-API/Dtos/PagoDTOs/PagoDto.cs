using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.PagoDetalleDTOs;

namespace ISP_API.Dtos.PagoDTOs;

public class PagoDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public DateTime FechaPago { get; set; }
    public decimal MontoPagado { get; set; }
    public decimal MontoTotal { get; set; }
    public decimal SaldoPendiente { get; set; }
    public bool EsPagoCompleto { get; set; }

    public ClienteDto? Cliente { get; set; }
    public List<PagoDetalleDto>? Detalles { get; set; }
}