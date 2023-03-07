using DocuSharp.Application.Abstractions;
using DocuSharp.Application.Services;
using DocuSharp.BackgroundService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = CreateHostBuilder(args).Build();

await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IAiService, AiService>();
            services.AddHostedService<DocGen>();
        });
}