using ISP_API.Services.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace ISP_API.Services;

public class SaldoBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SaldoBackgroundService> _logger;

    public SaldoBackgroundService(IServiceProvider serviceProvider, ILogger<SaldoBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SaldoBackgroundService iniciado ✅");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<PagoService>();
                    await service.ActualizarSaldosClientesAsync();
                }

                _logger.LogInformation("💰 Saldos actualizados automáticamente: {time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al actualizar los saldos");
            }

            // se actualiza cada 24 horas
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}