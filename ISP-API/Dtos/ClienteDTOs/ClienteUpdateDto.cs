namespace ISP_API.Dtos.ClienteDTOs;

public class ClienteUpdateDto
{
    public string CodigoCliente { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Identidad { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public decimal CostoInstalacion { get; set; }
    
    // IDs relacionados
    public List<Guid>? PlanesContratadosIds { get; set; }
    public List<Guid>? EquiposIds { get; set; }
}