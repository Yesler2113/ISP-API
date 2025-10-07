using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.ClientePlanDTOs;

public class ClientePlanDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PlanId { get; set; }
    
    public string ClienteNombre { get; set; }
    public string ClienteApellido { get; set; }      // 👈 nuevo
    public string ClienteDireccion { get; set; }     // 👈 nuevo
    public string ClienteTelefono { get; set; }      // 👈 nuevo
    public string ClienteCodigo { get; set; }

    public string PlanNombre { get; set; }
    public string PlanDescripcion { get; set; }
    public decimal PlanPrecio { get; set; }

    public DateTime FechaPago { get; set; }          // 👈 nuevo
}

