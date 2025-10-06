using ISP_API.Data.UserDTOs;
using ISP_API.Dtos.ClientePlanDTOs;
using ISP_API.Dtos.EquipoClienteDTOs;
using ISP_API.Dtos.PagoDTOs;

namespace ISP_API.Dtos.ClienteDTOs;

public class ClienteDto
{
    public Guid Id { get; set; }
    public string CodigoCliente { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Identidad { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public DateTime FechaInicio { get; set; }
    public decimal CostoInstalacion { get; set; }
    public List<ClientePlanDto>? PlanesContratados { get; set; }
    public List<EquipoClienteDto>? EquiposAsignados { get; set; }
    public List<PagoDto>? Pagos { get; set; }
    public LoginDto? Usuario { get; set; }
    
}