using ISP_API.Dtos;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Entities;

namespace ISP_API.Services.Entities;

public interface IClienteService
{
    Task<ResponseDto<ClienteDto>> CreateClienteAsync(ClienteCreateDto model);
    Task<ResponseDto<List<ClienteDto>>> GetAllClientesAsync();
    Task<ResponseDto<ClienteDto>> GetClienteByIdAsync(Guid id);
    Task<ResponseDto<ClienteDto>> UpdateClienteAsync(ClienteDto model);
    Task<ResponseDto<ClienteDto>> DeleteClienteAsync(Guid id);
    Task<ResponseDto<ClienteEntity>> EditarClienteAsync(Guid id, ClienteUpdateDto dto);
}