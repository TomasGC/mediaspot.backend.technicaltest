using FluentValidation;
using Mediaspot.Application.Common;
using Mediaspot.Application.Titles.Commands.Create;
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

        services.AddValidatorsFromAssembly(typeof(CreateTitleValidator).Assembly);

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateTitleValidator).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}