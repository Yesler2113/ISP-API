using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISP_API.Entities;

[Table("ClientePlan")]
public class ClientePlanEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("cliente_id")]
    public Guid ClienteId { get; set; }
    
    [ForeignKey("ClienteId")]
    public ClienteEntity? Cliente { get; set; }
    [Column("plan_id")]
    public Guid PlanId { get; set; }
    
    [ForeignKey("PlanId")]
    public PlanEntity? Plan { get; set; }
}