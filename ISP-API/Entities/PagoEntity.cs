using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;

[Table("pago")]
public class PagoEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("cliente_id")]
    public Guid ClienteId { get; set; }
    [ForeignKey("ClienteId")]
    public ClienteEntity? Cliente { get; set; }
    [Column("fecha_pago")]
    public DateTime FechaPago { get; set; } = DateTime.UtcNow;
    [Column("monto_pagado")]
    public decimal MontoPagado { get; set; }
    [Column("monto_total")]
    public decimal MontoTotal { get; set; }
    [Column("saldo_pendiente")]
    public decimal SaldoPendiente { get; set; }
    [Column("es_pago_completo")]
    public bool EsPagoCompleto { get; set; }
    
    //relacion con detalles de planes pagados
    [InverseProperty("Pago")]
    public ICollection<PagoDetalleEntity>? Detalles { get; set; }
}