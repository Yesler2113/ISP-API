using System.ComponentModel.DataAnnotations;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.PlanDTOs;

namespace ISP_API.Dtos.ClientePlanDTOs;

public class ClientePlanCreateDto
{
    [Display(Name = "Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public Guid ClienteId { get; set; }
    [Display(Name = "Plan")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public Guid PlanId { get; set; }
    public ClienteDto? Cliente { get; set; }
    public PlanDto? Plan { get; set; }
}