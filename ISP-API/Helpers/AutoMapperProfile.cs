using AutoMapper;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.EquipoDTOs;
using ISP_API.Dtos.PlanDTOs;
using ISP_API.Entities;

namespace ISP_API.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PlanCreateDto, PlanEntity>();
        CreateMap<PlanEntity, PlanDto>();
        CreateMap<EquipoCreateDto, EquipoEntity>();
        CreateMap<EquipoEntity, EquipoDto>();
        CreateMap<ClienteCreateDto, ClienteEntity>();
        CreateMap<ClienteEntity, ClienteDto>();
        
    }
}