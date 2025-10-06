using ISP_API.Dtos.EquipoClienteDTOs;

namespace ISP_API.Dtos.EquipoDTOs;

public class EquipoDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Cantidad { get; set; }

    public List<EquipoClienteDto>? Clientes { get; set; }
}
