using System.ComponentModel.DataAnnotations;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.ClientePlanDTOs;

public class ClientePlanCreateDto
{
    [Required]
    public Guid ClienteId { get; set; }
    [Required]
    public Guid PlanId { get; set; }
}