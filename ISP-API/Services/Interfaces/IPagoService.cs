using ISP_API.Dtos;
using ISP_API.Dtos.PagoDTOs;
using ISP_API.Entities;

namespace ISP_API.Services.Entities;

public interface IPagoService
{
    Task<byte[]> GenerarReciboPagoAsync(Guid pagoId);
    Task<ResponseDto<PagoEntity>> RegistrarPagoAsync(PagoCreateDto dto);
    Task<object> GetResumenDashboardAsync();
    Task ActualizarSaldosClientesAsync();
}