using AutoMapper;
using ISP_API.Data;
using ISP_API.Dtos;
using ISP_API.Dtos.PlanDTOs;
using ISP_API.Entities;
using ISP_API.Services.Entities;

namespace ISP_API.Services;

public class PlanService : IPlanService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public PlanService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    
    public async Task<ResponseDto<PlanDto>> CreatePlanAsync(PlanCreateDto model)
    {
        var planEntity = _mapper.Map<PlanEntity>(model);

        _context.PlanesServicio.Add(planEntity);
        await _context.SaveChangesAsync();
        
        var planDto = _mapper.Map<PlanDto>(planEntity);
        
        return new ResponseDto<PlanDto>
        {
            Status = true,
            StatusCode = 201,
            Message = "Plan created successfully",
            Data = planDto
        };
    }
    
    // traer todos los planes
    public async Task<ResponseDto<List<PlanDto>>> GetAllPlansAsync()
    {
        var plans =  _context.PlanesServicio.ToList();
        var planDtos = _mapper.Map<List<PlanDto>>(plans);
        return new ResponseDto<List<PlanDto>>
        {
            Status = true,
            StatusCode = 200,
            Message = "Plans retrieved successfully",
            Data = planDtos
        };
    }
    
    // traer un plan por id
    public async Task<ResponseDto<PlanDto>> GetPlanByIdAsync(Guid id)
    {
        var plan =  _context.PlanesServicio.FirstOrDefault(p => p.Id == id);
        if (plan == null)
        {
            return new ResponseDto<PlanDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Plan not found",
                Data = null
            };
        }
        var planDto = _mapper.Map<PlanDto>(plan);
        return new ResponseDto<PlanDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Plan retrieved successfully",
            Data = planDto
        };
    }
    
    // actualizar un plan
    public async Task<ResponseDto<PlanDto>> UpdatePlanAsync(PlanDto model)
    {
        var existingPlan =  _context.PlanesServicio.FirstOrDefault(p => p.Id == model.Id);
        if (existingPlan == null)
        {
            return new ResponseDto<PlanDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Plan not found",
                Data = null
            };
        }
        existingPlan.Nombre = model.Nombre;
        existingPlan.Descripcion = model.Descripcion;
        existingPlan.Precio = model.Precio;
        existingPlan.Tipo = model.Tipo;
        
        await _context.SaveChangesAsync();
        
        var planDto = _mapper.Map<PlanDto>(existingPlan);
        return new ResponseDto<PlanDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Plan updated successfully",
            Data = planDto
        };
        
        
    }
    // eliminar un plan
    public async Task<ResponseDto<PlanDto>> DeletePlanAsync(Guid id)
    {
        var existingPlan =  _context.PlanesServicio.FirstOrDefault(p => p.Id == id);
        if (existingPlan == null)
        {
            return new ResponseDto<PlanDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Plan not found",
                Data = null
            };
        }
        _context.PlanesServicio.Remove(existingPlan);
        await _context.SaveChangesAsync();
        return new ResponseDto<PlanDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Plan deleted successfully",
            Data = null
        };
    }

}

