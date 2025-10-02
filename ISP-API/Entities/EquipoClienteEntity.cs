using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ISP_API.Entities;

[Table("equipo_cliente")]
public class EquipoClienteEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("cliente_id")]
    public Guid ClienteId { get; set; }
    [ForeignKey("ClienteId")]
    public ClienteEntity? Cliente { get; set; }
    [Column("equipo_id")]
    public Guid EquipoId { get; set; }
    [ForeignKey("EquipoId")]
    public EquipoEntity? Equipo { get; set; }
    [Required]
    [Column("mac_address")]
    public string MacAddress { get; set; } = string.Empty;
}