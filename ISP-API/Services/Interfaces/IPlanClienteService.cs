using ISP_API.Dtos;
using ISP_API.Dtos.ClienteDetalles;
using ISP_API.Dtos.ClientePlanDTOs;

namespace ISP_API.Services.Entities;

public interface IPlanClienteService
{
    //Task<ResponseDto<List<ClientePlanDto>>> GetAllClientePlanesAsync();
    Task<ResponseDto<ClientePlanDetalleDto>> GetClientePlanByClienteIdAsync(Guid clienteId);
    Task<ResponseDto<IEnumerable<object>>> GetAllClientePlanesAsync();

}