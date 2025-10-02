using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;

[Table("equipos")]
public class EquipoEntity
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
    [Column("cantidad")]
    public int Cantidad { get; set; }
    //relacion con equipos asociados a clientes
    [InverseProperty("Equipo")]
    public ICollection<EquipoClienteEntity>? Clientes { get; set; }
}