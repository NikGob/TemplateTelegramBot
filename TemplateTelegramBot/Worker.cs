using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTelegramBot;


namespace TemplateTelegramBot;

public class Worker : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;

    public Worker(IHostApplicationLifetime appLifetime)
    {
        _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Service started.");
        _appLifetime.ApplicationStarted.Register(OnStarted);
        _appLifetime.ApplicationStopping.Register(OnStopping);
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        Console.WriteLine("Service is running.");
    }

    private void OnStopping()
    {
        Console.WriteLine("Service is stopping.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Service stopped.");
        return Task.CompletedTask;
    }
}


