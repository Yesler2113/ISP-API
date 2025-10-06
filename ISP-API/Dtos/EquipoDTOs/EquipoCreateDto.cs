using Microsoft.OpenApi.Attributes;

namespace ISP_API.Dtos.EquipoDTOs;

public class EquipoCreateDto
{
    [Display("Nombre")]
    public string Nombre { get; set; } = string.Empty;
    [Display("Descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    [Display("Cantidad")]
    public int Cantidad { get; set; }
}