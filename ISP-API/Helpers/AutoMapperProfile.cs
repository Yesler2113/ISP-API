using AutoMapper;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Dtos.ClientePlanDTOs;
using ISP_API.Dtos.EquipoClienteDTOs;
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
        CreateMap<ClientePlanEntity, ClientePlanDto>()
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.ClienteApellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
            .ForMember(dest => dest.ClienteDireccion, opt => opt.MapFrom(src => src.Cliente.Direccion))
            .ForMember(dest => dest.ClienteTelefono, opt => opt.MapFrom(src => src.Cliente.Telefono))
            .ForMember(dest => dest.ClienteCodigo, opt => opt.MapFrom(src => src.Cliente.CodigoCliente))
            .ForMember(dest => dest.PlanNombre, opt => opt.MapFrom(src => src.Plan.Nombre))
            .ForMember(dest => dest.PlanDescripcion, opt => opt.MapFrom(src => src.Plan.Descripcion))
            .ForMember(dest => dest.PlanPrecio, opt => opt.MapFrom(src => src.Plan.Precio));
        CreateMap<EquipoClienteEntity, EquipoClienteDto>()
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Equipo.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Equipo.Descripcion))
            .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Equipo.Cantidad));

        CreateMap<ClientePlanCreateDto, ClientePlanEntity>().ReverseMap();
        
    }
}