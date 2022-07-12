using Microsoft.Extensions.Hosting;

namespace Business.Utilities.BackgroundServices
{
    public class TestTimeWriter : IHostedService, IDisposable
    {
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(WriteTimerOnScreen, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void WriteTimerOnScreen(object? state)
        {
            Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer = null;
        }
    }
}
