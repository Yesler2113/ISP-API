using Microsoft.OpenApi.Attributes;

namespace ISP_API.Dtos.EquipoClienteDTOs;

public class EquipoClienteCreateDto
{
    [Display("Mac Address")]
    public string MacAddress { get; set; } = string.Empty;
}