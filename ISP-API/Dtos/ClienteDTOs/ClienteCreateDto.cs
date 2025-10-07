using System.ComponentModel.DataAnnotations;
using ISP_API.Data.UserDTOs;
using ISP_API.Dtos.ClientePlanDTOs;
using ISP_API.Dtos.EquipoClienteDTOs;
using ISP_API.Dtos.PagoDTOs;

namespace ISP_API.Dtos.ClienteDTOs;

public class ClienteCreateDto
{
    [Display(Name = "Codigo Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string CodigoCliente { get; set; }
    [Display(Name = "Nombre Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Nombre { get; set; }
    [Display(Name = "Apellido Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Apellido { get; set; }
    [Display(Name = "Identidad Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Identidad { get; set; }
    [Display(Name = "Direccion Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Direccion { get; set; }
    [Display(Name = "Telefono Cliente")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public string Telefono { get; set; }
    [Display(Name = "Fecha Inicio")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public DateTime FechaInicio { get; set; }
    [Display(Name = "Costo Instalacion")]
    [Required(ErrorMessage = "El campo {0} es obligatorio")]
    public decimal CostoInstalacion { get; set; }
    
    public List<Guid>? PlanesContratadosIds { get; set; }
    
    public List<Guid>? EquiposIds { get; set; }
    
}