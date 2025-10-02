using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;

[Table("pago_detalle")]
public class PagoDetalleEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("pago_id")]
    public Guid PagoId { get; set; }
    [ForeignKey("PagoId")]
    public PagoEntity? Pago { get; set; }
    [Column("plan_id")]
    public Guid PlanId { get; set; }
    [ForeignKey("PlanId")]
    public PlanEntity? Plan { get; set; }
    [Column("monto")]
    public decimal Monto { get; set; }
}