using ISP_API.Dtos.PagoDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.PagoDetalleDTOs;

public class PagoDetalleDto
{
    public Guid Id { get; set; }
    public Guid PagoId { get; set; }
    public Guid PlanId { get; set; }
    public decimal Monto { get; set; }

    public PagoDto? Pago { get; set; }
    public PlanDto? Plan { get; set; }
    
}