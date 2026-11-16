using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ProductsValidation.Services
{
    public class BrowserLaunchHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime lifetime;

        public BrowserLaunchHostedService(IHostApplicationLifetime lifetime)
        {
            this.lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            lifetime.ApplicationStarted.Register(() =>
            {
                Task.Delay(500, cancellationToken).ContinueWith(_ => OpenBrowser(), cancellationToken);
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OpenBrowser()
        {
            const string launchUrl = "http://localhost:5000";
            Process.Start(new ProcessStartInfo
            {
                FileName = launchUrl,
                UseShellExecute = true
            });
        }
    }
}
