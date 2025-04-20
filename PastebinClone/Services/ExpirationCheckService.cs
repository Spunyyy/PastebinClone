
using PastebinClone.Data;

namespace PastebinClone.Services
{
    public class ExpirationCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScope;

        public ExpirationCheckService(IServiceScopeFactory serviceScope)
        {
            this.serviceScope = serviceScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = serviceScope.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

                var expiredPastes = dbContext.Pastes.Where(p => p.ExpirationDate < DateTime.UtcNow).ToList();

                if (expiredPastes.Any())
                {
                    dbContext.RemoveRange(expiredPastes);
                    await dbContext.SaveChangesAsync();
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
