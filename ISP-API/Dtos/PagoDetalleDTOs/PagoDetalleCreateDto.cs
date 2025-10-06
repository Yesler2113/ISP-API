using System.ComponentModel.DataAnnotations;

namespace ISP_API.Dtos.PagoDetalleDTOs;

public class PagoDetalleCreateDto
{
    [Display(Name = "Pago monto")]
    public decimal Monto { get; set; }
}