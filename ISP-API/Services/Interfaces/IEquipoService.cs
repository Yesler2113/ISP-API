using ISP_API.Dtos;
using ISP_API.Dtos.EquipoDTOs;

namespace ISP_API.Services.Entities;

public interface IEquipoService
{
    Task<ResponseDto<EquipoDto>> CreateEquipoAsync(EquipoCreateDto model);
    Task<ResponseDto<List<EquipoDto>>> GetAllEquiposAsync();
    Task<ResponseDto<EquipoDto>> GetEquipoByIdAsync(Guid id);
    Task<ResponseDto<EquipoDto>> UpdateEquipoAsync(EquipoDto model);
    Task<ResponseDto<EquipoDto>> DeleteEquipoAsync(Guid id);

}