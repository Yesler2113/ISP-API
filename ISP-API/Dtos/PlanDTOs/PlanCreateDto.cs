

using System.ComponentModel.DataAnnotations;

namespace ISP_API.Dtos.PlanDTOs;

public class PlanCreateDto
{
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;
    [Display(Name = "Descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    [Display(Name = "Precio")]
    public decimal Precio { get; set; }
    [Display(Name = "Tipo")]
    public string Tipo { get; set; } = string.Empty;
}