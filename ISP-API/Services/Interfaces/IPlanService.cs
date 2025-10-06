using ISP_API.Dtos;
using ISP_API.Dtos.PlanDTOs;
using ISP_API.Entities;

namespace ISP_API.Services.Entities;

public interface IPlanService
{
    Task<ResponseDto<PlanDto>> CreatePlanAsync(PlanCreateDto model);
    Task<ResponseDto<List<PlanDto>>> GetAllPlansAsync();
    Task<ResponseDto<PlanDto>> GetPlanByIdAsync(Guid id);
    Task<ResponseDto<PlanDto>> UpdatePlanAsync(PlanDto model);
    Task<ResponseDto<PlanDto>> DeletePlanAsync(Guid id);

}