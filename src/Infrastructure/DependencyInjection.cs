using FluentValidation;
using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Common;
using Mediaspot.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mediaspot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseName)
    {
        services.AddDbContext<MediaspotDbContext>(o => o.UseInMemoryDatabase(databaseName));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITitleRepository, TitleRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<ITranscodeJobRepository, TranscodeJobRepository>();

        // MediatR

        services.AddValidatorsFromAssembly(typeof(CreateAssetValidator).Assembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateAssetCommand).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}