# ISP-API - Sistema de Gestión para Proveedores de Internet

API RESTful desarrollada en ASP.NET Core 8.0 para la gestión integral de clientes, planes de internet, equipos y pagos de un proveedor de servicios de internet (ISP).

## Características Principales

- Gestión completa de clientes (CRUD)
- Administración de planes de internet
- Control de equipos y asignación a clientes
- Sistema de registro de pagos con generación de recibos en PDF
- Actualización automática de saldos pendientes
- Autenticación y autorización con JWT
- Dashboard con resumen financiero
- Documentación interactiva con Swagger y Scalar

## Tecnologías Utilizadas

- **Framework**: ASP.NET Core 8.0
- **Base de Datos**: PostgreSQL con Entity Framework Core
- **Autenticación**: JWT Bearer + ASP.NET Identity
- **Mapeo de Objetos**: AutoMapper
- **Generación de PDFs**: QuestPDF
- **Documentación API**: Swagger + Scalar
- **ORM**: Entity Framework Core 8.0

## Estructura del Proyecto

```
ISP-API/
├── Controllers/          # Controladores de la API
│   ├── AccountController.cs       # Autenticación y registro
│   ├── ClientController.cs        # Gestión de clientes
│   ├── PlanController.cs          # Gestión de planes
│   ├── EquipoController.cs        # Gestión de equipos
│   ├── PagosController.cs         # Registro de pagos
│   └── ClientePlanController.cs   # Relación cliente-plan
├── Data/                 # Contexto de base de datos
│   └── AppDbContext.cs
├── Entities/             # Modelos de datos
│   ├── ClienteEntity.cs
│   ├── PlanEntity.cs
│   ├── EquipoEntity.cs
│   ├── PagoEntity.cs
│   ├── UserEntity.cs
│   └── ...
├── Dtos/                 # Data Transfer Objects
├── Services/             # Lógica de negocio
│   ├── ClienteService.cs
│   ├── PlanService.cs
│   ├── EquipoService.cs
│   ├── PagoService.cs
│   └── UserService.cs
├── Migrations/           # Migraciones de base de datos
└── Program.cs           # Punto de entrada
```

## Requisitos Previos

- .NET 8.0 SDK o superior
- PostgreSQL 12 o superior
- Visual Studio 2022, Rider o VS Code

## Configuración

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd ISP-API
```

### 2. Configurar la Base de Datos

Crear un archivo `.env` o modificar `appsettings.json` con tu cadena de conexión a PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=isp_db;Username=tu_usuario;Password=tu_password"
  },
  "JWT": {
    "ValidAudience": "http://localhost:5000",
    "ValidIssuer": "http://localhost:5000",
    "Secret": "tu_clave_secreta_muy_segura_de_al_menos_32_caracteres"
  }
}
```

### 3. Ejecutar Migraciones

```bash
dotnet ef database update
```

### 4. Instalar Dependencias

```bash
dotnet restore
```

### 5. Ejecutar la Aplicación

```bash
dotnet run
```

La API estará disponible en `http://localhost:5000` (o el puerto configurado).

## Endpoints de la API

### Autenticación

#### Registrar Usuario
```http
POST /api/account/Register
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "Password123!",
  "userName": "usuario"
}
```

#### Iniciar Sesión
```http
POST /api/account/Login
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "Password123!"
}
```

**Respuesta:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "expiration": "2024-10-10T12:00:00Z"
}
```

### Clientes

#### Crear Cliente
```http
POST /api/clientes
Authorization: Bearer {token}
Content-Type: application/json

{
  "codigoCliente": "CLI-001",
  "nombre": "Juan",
  "apellido": "Pérez",
  "identidad": "0801199012345",
  "direccion": "Col. Kennedy, Tegucigalpa",
  "telefono": "99887766",
  "fechaInicio": "2024-01-15",
  "costoInstalacion": 500.00,
  "planesContratadosIds": ["guid-del-plan"],
  "equiposIds": ["guid-del-equipo"]
}
```

#### Obtener Todos los Clientes
```http
GET /api/clientes/todos
Authorization: Bearer {token}
```

#### Obtener Cliente por ID
```http
GET /api/clientes/{id}
Authorization: Bearer {token}
```

#### Actualizar Cliente
```http
PUT /api/clientes/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "nombre": "Juan Carlos",
  "apellido": "Pérez López",
  "telefono": "99887766"
}
```

#### Eliminar Cliente
```http
DELETE /api/clientes/{id}
Authorization: Bearer {token}
```

### Planes

#### Crear Plan
```http
POST /api/planes
Authorization: Bearer {token}
Content-Type: application/json

{
  "nombre": "Plan Básico 10Mbps",
  "descripcion": "Plan residencial con 10Mbps de velocidad",
  "precio": 300.00,
  "tipo": "Residencial"
}
```

#### Obtener Todos los Planes
```http
GET /api/planes
Authorization: Bearer {token}
```

#### Obtener Plan por ID
```http
GET /api/planes/{id}
Authorization: Bearer {token}
```

#### Actualizar Plan
```http
PUT /api/planes/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "nombre": "Plan Básico 15Mbps",
  "precio": 350.00
}
```

#### Eliminar Plan
```http
DELETE /api/planes/{id}
Authorization: Bearer {token}
```

### Equipos

#### Crear Equipo
```http
POST /api/equipos
Authorization: Bearer {token}
Content-Type: application/json

{
  "nombre": "Router TP-Link AC1200",
  "tipo": "Router",
  "marca": "TP-Link",
  "modelo": "Archer C6",
  "numeroSerie": "SN123456789",
  "estado": "Disponible"
}
```

#### Obtener Todos los Equipos
```http
GET /api/equipos
Authorization: Bearer {token}
```

#### Obtener Equipo por ID
```http
GET /api/equipos/{id}
Authorization: Bearer {token}
```

#### Actualizar Equipo
```http
PUT /api/equipos/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "estado": "Asignado"
}
```

#### Eliminar Equipo
```http
DELETE /api/equipos/{id}
Authorization: Bearer {token}
```

### Pagos

#### Preparar Información de Pago
```http
GET /api/pagos/preparar/{clienteId}
Authorization: Bearer {token}
```

Devuelve el monto total a pagar basado en los planes contratados del cliente.

#### Registrar Pago
```http
POST /api/pagos/registrar
Authorization: Bearer {token}
Content-Type: application/json

{
  "clienteId": "guid-del-cliente",
  "fechaPago": "2024-10-10",
  "montoTotal": 300.00,
  "montoPagado": 300.00,
  "saldoPendiente": 0.00,
  "esPagoCompleto": true,
  "detalles": [
    {
      "planId": "guid-del-plan",
      "monto": 300.00,
      "descripcion": "Plan Básico 10Mbps - Octubre 2024"
    }
  ]
}
```

**Respuesta:** Devuelve un archivo PDF con el recibo del pago.

#### Generar Recibo de Pago
```http
GET /api/pagos/recibo/{pagoId}
Authorization: Bearer {token}
```

**Respuesta:** Archivo PDF del recibo.

#### Obtener Resumen del Dashboard
```http
GET /api/pagos/dashboard/resumen
Authorization: Bearer {token}
```

**Respuesta:**
```json
{
  "totalClientes": 150,
  "totalRecaudado": 45000.00,
  "clientesAlDia": 130,
  "clientesMorosos": 20,
  "ingresosMesActual": 15000.00
}
```

#### Actualizar Saldos de Clientes
```http
GET /api/pagos/actualizar-saldos
Authorization: Bearer {token}
```

Este endpoint actualiza los saldos pendientes de todos los clientes basándose en su fecha de corte.

## Modelo de Datos

### Cliente
- **Id**: Identificador único (GUID)
- **CodigoCliente**: Código único del cliente
- **Nombre**: Nombre del cliente
- **Apellido**: Apellido del cliente
- **Identidad**: Número de identidad
- **Direccion**: Dirección física
- **Telefono**: Número de teléfono
- **FechaInicio**: Fecha de inicio del servicio
- **CostoInstalacion**: Costo de instalación inicial
- **FechaPago**: Día de corte mensual
- **SaldoActual**: Saldo pendiente actual
- **ProximoPago**: Fecha del próximo pago

### Plan
- **Id**: Identificador único (GUID)
- **Nombre**: Nombre del plan
- **Descripcion**: Descripción del plan
- **Precio**: Precio mensual
- **Tipo**: Tipo de plan (Residencial, Empresarial, etc.)

### Equipo
- **Id**: Identificador único (GUID)
- **Nombre**: Nombre del equipo
- **Tipo**: Tipo de equipo (Router, ONT, etc.)
- **Marca**: Marca del fabricante
- **Modelo**: Modelo del equipo
- **NumeroSerie**: Número de serie
- **Estado**: Estado actual (Disponible, Asignado, En reparación)

### Pago
- **Id**: Identificador único (GUID)
- **ClienteId**: ID del cliente
- **FechaPago**: Fecha del pago
- **MontoTotal**: Monto total a pagar
- **MontoPagado**: Monto pagado
- **SaldoPendiente**: Saldo pendiente después del pago
- **EsPagoCompleto**: Indica si el pago fue completo
- **Detalles**: Lista de conceptos pagados

## Funcionalidades Avanzadas

### Generación Automática de Recibos PDF

El sistema genera automáticamente recibos en PDF al registrar un pago utilizando QuestPDF. Los recibos incluyen:
- Información del cliente
- Detalles del pago
- Lista de conceptos pagados
- Total pagado y saldo pendiente

### Actualización Automática de Saldos

El sistema incluye un proceso que puede ejecutarse periódicamente para:
- Actualizar los saldos pendientes de los clientes
- Agregar el costo de los planes contratados en la fecha de corte
- Actualizar la fecha del próximo pago

### Dashboard Financiero

El endpoint de dashboard proporciona métricas clave como:
- Total de clientes activos
- Total recaudado
- Clientes al día vs. morosos
- Ingresos del mes actual

## Documentación Interactiva

El proyecto incluye documentación interactiva accesible en:

- **Swagger UI**: `http://localhost:5000/swagger`
- **Scalar UI**: `http://localhost:5000/scalar/v1`

Scalar proporciona una interfaz moderna y atractiva para explorar y probar la API.

## Seguridad

- Autenticación basada en JWT (JSON Web Tokens)
- Tokens con tiempo de expiración configurable
- Contraseñas hasheadas con ASP.NET Identity
- CORS configurado para permitir acceso desde cualquier origen (ajustar en producción)
- Validación de modelos en todos los endpoints

## Docker

El proyecto incluye un `Dockerfile` para contenarización:

```bash
docker build -t isp-api .
docker run -p 5000:8080 isp-api
```

## Scripts de Base de Datos

### Aplicar Migraciones
```bash
dotnet ef database update
```

### Crear Nueva Migración
```bash
dotnet ef migrations add NombreDeLaMigracion
```

### Revertir Última Migración
```bash
dotnet ef database update AnteriorMigracion
```

## Desarrollo

### Agregar Nuevo Controlador

1. Crear el controlador en `/Controllers`
2. Crear el servicio en `/Services`
3. Crear los DTOs en `/Dtos`
4. Registrar el servicio en `Startup.cs`

### Buenas Prácticas

- Usar DTOs para todas las transferencias de datos
- Implementar validaciones en los DTOs
- Manejar excepciones de forma centralizada
- Usar AutoMapper para mapeo de entidades
- Documentar los endpoints con atributos XML

## Testing

Para ejecutar las pruebas unitarias (si están disponibles):

```bash
dotnet test
```

## Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/NuevaFuncionalidad`)
3. Commit tus cambios (`git commit -m 'Agregar nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/NuevaFuncionalidad`)
5. Abrir un Pull Request

## Licencia

Este proyecto está licenciado bajo la Licencia MIT.

## Contacto

Para preguntas o soporte, contactar al equipo de desarrollo.

## Notas Adicionales

- El sistema utiliza PostgreSQL como base de datos principal
- Los GUIDs se generan automáticamente para todas las entidades
- Las fechas se manejan en UTC
- Los precios se almacenan como tipo `decimal` para mayor precisión
- El sistema soporta múltiples planes por cliente
- Los equipos pueden ser asignados y reasignados a diferentes clientes

## Roadmap

- [ ] Implementar notificaciones por email
- [ ] Agregar reportes mensuales en Excel
- [ ] Sistema de tickets de soporte técnico
- [ ] Panel de administración web
- [ ] Integración con pasarelas de pago
- [ ] Sistema de suspensión automática por mora
- [ ] Aplicación móvil para clientes
