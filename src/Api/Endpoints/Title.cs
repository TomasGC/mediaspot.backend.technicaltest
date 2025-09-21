using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Commands.UpdateMetadata;
using Mediaspot.Application.Titles.Queries.GetByExternalId;
using Mediaspot.Application.Titles.Queries.GetById;
using Mediaspot.Application.Titles.Queries.List;
using MediatR;

namespace Mediaspot.Api.Endpoints;

public static class Title
{
    private const string API_ENDPOINT = "/titles";

    public static WebApplication MapTitleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(API_ENDPOINT).WithTags("Titles");

        group.MapGet("{id:guid}", async (Guid id, ISender sender) =>
            {
                var title = await sender.Send(new GetTitleByIdQuery(id));
                return Results.Ok(title);
            })
            .WithName("GetTitleById")
            .WithOpenApi();

        group.MapGet("{externalId}", async (string externalId, ISender sender) =>
            {
                var title = await sender.Send(new GetTitleByExternalIdQuery(externalId));
                return Results.Ok(title);
            })
            .WithName("GetTitleByExternalId")
            .WithOpenApi();

        group.MapGet("", async ([AsParameters] ListTitlesQuery query, ISender sender) =>
            {
                var title = await sender.Send(query);
                return Results.Ok(title);
            })
            .WithName("ListTitles")
            .WithOpenApi();

        group.MapPost("", async (CreateTitleDto request, ISender sender) =>
            {
                var cmd = new CreateTitleCommand(
                    request.ExternalId,
                    request.Type,
                    request.Name,
                    new(request.OriginCountry, request.OriginalLanguage),
                    request.Description,
                    request.SeasonNumber);

                return Results.Created(API_ENDPOINT, new { id = await sender.Send(cmd) });
            })
            .WithName("PostCreateTitle")
            .WithOpenApi();

        group.MapPut("{id:guid}/metadata", async (Guid id, UpdateTitleMetadataDto dto, ISender sender) =>
            {
                var cmd = new UpdateTitleMetadataCommand(
                    id,
                    dto.Name,
                    dto.OriginCountry,
                    dto.OriginalLanguage,
                    dto.Description,
                    dto.SeasonNumber);

                await sender.Send(cmd);
                return Results.NoContent();
            })
            .WithName("PutUpdateTitleMetadata")
            .WithOpenApi();

        return app;
    }
}
