using System.ComponentModel.DataAnnotations;
using ISP_API.Dtos.PagoDetalleDTOs;

namespace ISP_API.Dtos.PagoDTOs;

public class PagoCreateDto
{
    [Required]
    public Guid ClienteId { get; set; }

    [Required]
    public decimal MontoPagado { get; set; }

    [Required]
    public decimal MontoTotal { get; set; }

    public decimal SaldoPendiente { get; set; }
    public bool EsPagoCompleto { get; set; }
    public List<PagoDetalleCreateDto> Detalles { get; set; } = new();
}