using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.EquipoDTOs;

namespace ISP_API.Dtos.EquipoClienteDTOs;

public class EquipoClienteDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public string MacAddress { get; set; } = string.Empty;
}
