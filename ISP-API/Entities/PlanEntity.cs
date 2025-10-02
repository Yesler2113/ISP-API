using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;

[Table("plan")]
public class PlanEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Required]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;
    [Column("descripcion")]
    public string Descripcion { get; set; } = string.Empty;
    [Required]
    [Column("precio")]
    public decimal Precio { get; set; }
    [Required]
    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;
    
    //relacion con clientes que tienen este plan
    [InverseProperty("Plan")]
    public ICollection<ClientePlanEntity>? Clientes { get; set; }
}