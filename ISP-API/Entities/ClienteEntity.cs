using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;
[Table("clientes")]
public class ClienteEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Required]
    [Column("codigo_cliente")]
    public string CodigoCliente { get; set; } = string.Empty;
    [Required]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;
    [Required]
    [Column("apellido")]
    public string Apellido { get; set; } = string.Empty;
    [Required]
    [Column("identidad")]
    public string Identidad { get; set; } = string.Empty;
    [Required]
    [Column("direccion")]
    public string Direccion { get; set; } = string.Empty;
    [Column("telefono")]
    public string Telefono { get; set; } = string.Empty;
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    [Column("costo_instalacion")]
    public decimal CostoInstalacion { get; set; }
    [Column("fecha_pago")]
    public DateTime FechaPago { get; set; } = DateTime.UtcNow; //dia de facturacion o corte
    [Column("saldo_actual")]
    public decimal SaldoActual { get; set; } = 0;

    [Column("proximo_pago")]
    public DateTime ProximoPago { get; set; } = DateTime.UtcNow.AddMonths(1);
    //relacion con Planes
    [InverseProperty("Cliente")]
    public ICollection<ClientePlanEntity>? PlanesContratados { get; set; }
    //relacion con Equipos
    [InverseProperty("Cliente")]
    public ICollection<EquipoClienteEntity>? EquiposAsignados { get; set; }
    //relacion con Pagos
    [InverseProperty("Cliente")]
    public ICollection<PagoEntity>? Pagos { get; set; }
    //usuario que lo registro
    [ForeignKey("UsuarioId")]
    public Guid UsuarioId { get; set; }
    public UserEntity? Usuario { get; set; }
}