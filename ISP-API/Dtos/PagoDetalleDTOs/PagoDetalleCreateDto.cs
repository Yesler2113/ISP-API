using System.ComponentModel.DataAnnotations;

namespace ISP_API.Dtos.PagoDetalleDTOs;

public class PagoDetalleCreateDto
{
    [Required]
    public Guid PlanId { get; set; }
    [Required]
    public decimal Monto { get; set; }
}