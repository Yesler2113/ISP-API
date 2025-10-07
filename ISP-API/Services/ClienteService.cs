using AutoMapper;
using ISP_API.Data;
using ISP_API.Dtos;
using ISP_API.Dtos.ClienteDTOs;
using ISP_API.Entities;
using ISP_API.Services.Entities;

namespace ISP_API.Services;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ClienteService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    
    public async Task<ResponseDto<ClienteDto>> CreateClienteAsync(ClienteCreateDto model)
    {
        var clienteEntity = _mapper.Map<ClienteEntity>(model);

        _context.Clientes.Add(clienteEntity);
        await _context.SaveChangesAsync();
        
        var clienteDto = _mapper.Map<ClienteDto>(clienteEntity);
        
        return new ResponseDto<ClienteDto>
        {
            Status = true,
            StatusCode = 201,
            Message = "Cliente created successfully",
            Data = clienteDto
        };
    }
    
    public async Task<ResponseDto<List<ClienteDto>>> GetAllClientesAsync()
    {
        var clientes =  _context.Clientes.ToList();
        var clienteDtos = _mapper.Map<List<ClienteDto>>(clientes);
        return new ResponseDto<List<ClienteDto>>
        {
            Status = true,
            StatusCode = 200,
            Message = "Clientes retrieved successfully",
            Data = clienteDtos
        };
    }
    
    public async Task<ResponseDto<ClienteDto>> GetClienteByIdAsync(Guid id)
    {
        var cliente =  _context.Clientes.FirstOrDefault(p => p.Id == id);
        if (cliente == null)
        {
            return new ResponseDto<ClienteDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Cliente not found",
                Data = null
            };
        }
        var clienteDto = _mapper.Map<ClienteDto>(cliente);
        return new ResponseDto<ClienteDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Cliente retrieved successfully",
            Data = clienteDto
        };
    }
    
    public async Task<ResponseDto<ClienteDto>> UpdateClienteAsync(ClienteDto model)
    {
        var existingCliente = _context.Clientes.FirstOrDefault(p => p.Id == model.Id);
        if (existingCliente == null)
        {
            return new ResponseDto<ClienteDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Cliente not found",
                Data = null
            };
        }

        existingCliente.Nombre = model.Nombre;
        existingCliente.Apellido = model.Apellido;
        existingCliente.Telefono = model.Telefono;
        existingCliente.Direccion = model.Direccion;

        _context.Clientes.Update(existingCliente);
        await _context.SaveChangesAsync();

        var clienteDto = _mapper.Map<ClienteDto>(existingCliente);
        return new ResponseDto<ClienteDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Cliente updated successfully",
            Data = clienteDto
        };
    }
    
    public async Task<ResponseDto<ClienteDto>> DeleteClienteAsync(Guid id)
    {
        var existingCliente = _context.Clientes.FirstOrDefault(p => p.Id == id);
        if (existingCliente == null)
        {
            return new ResponseDto<ClienteDto>
            {
                Status = false,
                StatusCode = 404,
                Message = "Cliente not found",
                Data = null
            };
        }

        _context.Clientes.Remove(existingCliente);
        await _context.SaveChangesAsync();

        var clienteDto = _mapper.Map<ClienteDto>(existingCliente);
        return new ResponseDto<ClienteDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Cliente deleted successfully",
            Data = clienteDto
        };
    }
}