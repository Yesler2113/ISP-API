using System.ComponentModel.DataAnnotations;

namespace ISP_API.Dtos.PagoDTOs;

public class PagoCreateDto
{
    [Display(Name = "fecha de pago")]
    public DateTime FechaPago { get; set; }
    [Display(Name = "monto pagado")]
    public decimal MontoPagado { get; set; }
    [Display(Name = "monto total")]
    public decimal MontoTotal { get; set; }
    [Display(Name = "saldo pendiente")]
    public decimal SaldoPendiente { get; set; }
    [Display(Name = "es pago completo")]
    public bool EsPagoCompleto { get; set; }
}