using AutoMapper;
using ISP_API.Data;
using ISP_API.Dtos;
using ISP_API.Dtos.ClienteDetalles;
using ISP_API.Dtos.ClientePlanDTOs;
using ISP_API.Dtos.EquipoDTOs;
using ISP_API.Dtos.PlanDTOs;
using ISP_API.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISP_API.Services;

public class PlanClienteService : IPlanClienteService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public PlanClienteService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        
    }
    //obtener todos los registros de ClientePlan
    public async Task<ResponseDto<IEnumerable<object>>> GetAllClientePlanesAsync()
    {
        var relaciones = await _context.Planes
            .Include(cp => cp.Cliente).ThenInclude(c => c.EquiposAsignados!).ThenInclude(eq => eq.Equipo)
            .Include(cp => cp.Plan)
            .ToListAsync();

        //var dtoList = _mapper.Map<List<ClientePlanDto>>(relaciones);
        var data = relaciones
            .GroupBy(cp => cp.ClienteId) // agrupar por cliente
            .Select(g => new
            {
                ClienteId = g.Key,
                ClienteNombre = g.First().Cliente!.Nombre,
                ClienteApellido = g.First().Cliente!.Apellido,
                ClienteDireccion = g.First().Cliente!.Direccion,
                ClienteTelefono = g.First().Cliente!.Telefono,
                ClienteCodigo = g.First().Cliente!.CodigoCliente,
                FechaPago = g.First().Cliente!.FechaPago,
                // 🔹 Lista de planes contratados
                Planes = g.Select(cp => new
                {
                    cp.Plan!.Id,
                    cp.Plan.Nombre,
                    cp.Plan.Descripcion,
                    cp.Plan.Precio
                }).ToList(),
                // 🔹 Lista de equipos asignados
                Equipos = g.First().Cliente!.EquiposAsignados!.Select(eq => new
                {
                    eq.Id,
                    eq.MacAddress,
                    eq.Equipo!.Nombre,
                    eq.Equipo.Descripcion,
                }).ToList()
            });

        return new ResponseDto<IEnumerable<object>>
        {
            Status = true,
            StatusCode = 200,
            Message = "Relaciones cliente-plan obtenidas correctamente",
            Data = data
        };
    }
    // Obtener los planes contratados por un cliente específico
    public async Task<ResponseDto<ClientePlanDetalleDto>> GetClientePlanByClienteIdAsync(Guid clienteId)
    {
        var cliente = await _context.Clientes
            .Include(c => c.PlanesContratados!)
            .ThenInclude(cp => cp.Plan)
            .Include(c => c.EquiposAsignados!)
            .ThenInclude(eq => eq.Equipo)
            .FirstOrDefaultAsync(c => c.Id == clienteId);

        if (cliente == null)
        {
            return new ResponseDto<ClientePlanDetalleDto>
            {
                Status = false,
                Message = "Cliente no encontrado."
            };
        }

        // Mapeo manual a DTO (para incluir planes y equipos)
        var dto = new ClientePlanDetalleDto
        {
            ClienteId = cliente.Id,
            ClienteNombre = cliente.Nombre,
            ClienteApellido = cliente.Apellido,
            ClienteDireccion = cliente.Direccion,
            ClienteTelefono = cliente.Telefono,
            ClienteCodigo = cliente.CodigoCliente,
            FechaPago = cliente.FechaPago,
            Planes = cliente.PlanesContratados!.Select(cp => new PlanDto
            {
                Id = cp.Plan!.Id,
                Nombre = cp.Plan.Nombre,
                Descripcion = cp.Plan.Descripcion,
                Precio = cp.Plan.Precio
            }).ToList(),
            Equipos = cliente.EquiposAsignados!.Select(eq => new EquipoDto
            {
                Id = eq.Equipo!.Id,
                Nombre = eq.Equipo.Nombre,
                Descripcion = eq.Equipo.Descripcion,
                Cantidad = eq.Equipo.Cantidad,
            }).ToList()
        };

        return new ResponseDto<ClientePlanDetalleDto>
        {
            Status = true,
            StatusCode = 200,
            Message = "Planes y equipos del cliente obtenidos correctamente",
            Data = dto
        };
    }


}