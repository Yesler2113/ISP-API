using System.Text;
using System.Text.Json.Serialization;
using ISP_API.Data;
using ISP_API.Entities;
using ISP_API.Services;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace ISP_API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly string _corsPolicy = "CorsPolicy";

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //configure services
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.


        // CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy(name: _corsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        // Database connection
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
        // add custom services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IPlanService, PlanService>();
        services.AddTransient<IEquipoService, EquipoService>();
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IPlanClienteService, PlanClienteService>();
        
        // AutoMapper
        services.AddAutoMapper(typeof(Startup));
        


        // Services
        services.AddControllers().AddJsonOptions(options =>
        {
           options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        //add Identity
        services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        //add authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            };
        });

    }
    
    //use Scalar
    
    // configure middleware
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
       
        app.UseSwagger(); 
        
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        
        //app.UseHttpsRedirection();
        app.MapScalarApiReference(opt =>
        {
            opt.Title = "Ejemplo escalar";
            opt.Theme = ScalarTheme.Purple;
            opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);

            opt.OpenApiRoutePattern= "/swagger/v1/swagger.json";
        });
        app.UseCors(_corsPolicy);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}