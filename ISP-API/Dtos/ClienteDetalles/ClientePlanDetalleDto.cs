using ISP_API.Dtos.EquipoDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.ClienteDetalles;

public class ClientePlanDetalleDto
{
    public Guid ClienteId { get; set; }
    public string ClienteNombre { get; set; } = string.Empty;
    public string ClienteApellido { get; set; } = string.Empty;
    public string ClienteDireccion { get; set; } = string.Empty;
    public string ClienteTelefono { get; set; } = string.Empty;
    public string ClienteCodigo { get; set; } = string.Empty;
    public DateTime FechaPago { get; set; }
    public List<PlanDto> Planes { get; set; } = new();
    public List<EquipoDto> Equipos { get; set; } = new();
}