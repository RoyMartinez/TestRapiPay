using Domain.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UFEHostedServices : IHostedService,IDisposable
    {
        private Timer _timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SetNewFee,null,TimeSpan.Zero,TimeSpan.FromSeconds(120));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void SetNewFee(Object state) 
        {
            UFE.GetUFE().SetFee();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
