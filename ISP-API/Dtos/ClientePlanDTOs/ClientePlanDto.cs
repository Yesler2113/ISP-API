using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.ClientePlanDTOs;

public class ClientePlanDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid PlanId { get; set; }
    public ClienteDto? Cliente { get; set; }
    public PlanDto? Plan { get; set; }
}