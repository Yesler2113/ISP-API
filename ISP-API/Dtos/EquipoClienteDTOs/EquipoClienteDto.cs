using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.EquipoDTOs;

namespace ISP_API.Dtos.EquipoClienteDTOs;

public class EquipoClienteDto
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Guid EquipoId { get; set; }
    public string MacAddress { get; set; } = string.Empty;
    public ClienteDto? Cliente { get; set; }
    public EquipoDto? Equipo { get; set; }
}