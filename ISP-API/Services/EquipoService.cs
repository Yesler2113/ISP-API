using AutoMapper;
using ISP_API.Data;
using ISP_API.Dtos;
using ISP_API.Dtos.EquipoDTOs;
using ISP_API.Entities;
using ISP_API.Services.Entities;

namespace ISP_API.Services;

public class EquipoService : IEquipoService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public EquipoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    public async Task<ResponseDto<EquipoDto>> CreateEquipoAsync(EquipoCreateDto model)
    {
        var equipoEntity = _mapper.Map<EquipoEntity>(model);

        _context.Equipos.Add(equipoEntity);
        await _context.SaveChangesAsync();
        
        var equipoDto = _mapper.Map<EquipoDto>(equipoEntity);
        
        return new ResponseDto<EquipoDto>
        {
            Status = true,
            StatusCode = 201,
            Message = "Equipo created successfully",
            Data = equipoDto
        };
    }
    
    public async Task<ResponseDto<List<EquipoDto>>> GetAllEquiposAsync()
    {
        var equipos =  _context.Equipos.ToList();
        var equipoDtos = _mapper.Map<List<EquipoDto>>(equipos);
        return new ResponseDto<List<EquipoDto>>
        {
            Status = true,
            StatusCode = 200,
            Message = "Equipos retrieved successfully",
            Data = equipoDtos
        };
    }
    
    public async Task<ResponseDto<EquipoDto>> GetEquipoByIdAsync(Guid id)
    {
        var equipo =  _context.Equipos.FirstOrDefault(p => p.Id == id);
        if (equipo == null)
        {
            return new ResponseDto<EquipoDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Equipo not found",
                Data = null
            };
        }
        var equipoDto = _mapper.Map<EquipoDto>(equipo);
        return new ResponseDto<EquipoDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Equipo retrieved successfully",
            Data = equipoDto
        };
    }
    
    public async Task<ResponseDto<EquipoDto>> UpdateEquipoAsync(EquipoDto model)
    {
        var existingEquipo = _context.Equipos.FirstOrDefault(p => p.Id == model.Id);
        if (existingEquipo == null)
        {
            return new ResponseDto<EquipoDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Equipo not found",
                Data = null
            };
        }

        existingEquipo.Nombre = model.Nombre;
        existingEquipo.Descripcion = model.Descripcion;
        existingEquipo.Cantidad = model.Cantidad;

        _context.Equipos.Update(existingEquipo);
        await _context.SaveChangesAsync();

        var equipoDto = _mapper.Map<EquipoDto>(existingEquipo);
        return new ResponseDto<EquipoDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Equipo updated successfully",
            Data = equipoDto
        };
    }
    
    public async Task<ResponseDto<EquipoDto>> DeleteEquipoAsync(Guid id)
    {
        var existingEquipo = _context.Equipos.FirstOrDefault(p => p.Id == id);
        if (existingEquipo == null)
        {
            return new ResponseDto<EquipoDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Equipo not found",
                Data = null
            };
        }

        _context.Equipos.Remove(existingEquipo);
        await _context.SaveChangesAsync();

        var equipoDto = _mapper.Map<EquipoDto>(existingEquipo);
        return new ResponseDto<EquipoDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Equipo deleted successfully",
            Data = equipoDto
        };
    }
    
}