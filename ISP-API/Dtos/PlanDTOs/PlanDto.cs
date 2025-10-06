using ISP_API.Dtos.ClientePlanDTOs;

namespace ISP_API.Dtos.PlanDTOs;

public class PlanDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Tipo { get; set; } = string.Empty;

    public List<ClientePlanDto>? Clientes { get; set; }
}