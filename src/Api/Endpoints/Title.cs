using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Api.Responses.Titles;
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
                return Results.Ok(new GetTitleResponse(
                    title.Id,
                    title.ExternalId,
                    title.Type,
                    new(
                        title.Metadata.Name,
                        new(title.Metadata.Origin.Country, title.Metadata.Origin.Language),
                        title.Metadata.Description,
                        title.Metadata.SeasonNumber),
                    title.ReleaseDate));
            })
            .WithName("GetTitleById")
            .WithOpenApi();

        group.MapGet("{externalId}", async (string externalId, ISender sender) =>
            {
                var title = await sender.Send(new GetTitleByExternalIdQuery(externalId));
                return Results.Ok(new GetTitleByExternalIdResponse(
                    title.Id,
                    title.ExternalId,
                    title.Type,
                    new(
                        title.Metadata.Name,
                        new(title.Metadata.Origin.Country, title.Metadata.Origin.Language),
                        title.Metadata.Description,
                        title.Metadata.SeasonNumber),
                    title.ReleaseDate));
            })
            .WithName("GetTitleByExternalId")
            .WithOpenApi();

        group.MapGet("", async ([AsParameters] ListTitlesDto request, ISender sender) =>
            {
                var query = new ListTitlesQuery(
                    request.Type,
                    request.FromReleaseDate,
                    request.ToReleaseDate,
                    request.NamePattern,
                    request.OriginCountry,
                    request.OriginalLanguage,
                    request.Page,
                    request.PageSize);

                List<Responses.Titles.Models.Title> titles = [];
                foreach (var title in await sender.Send(query))
                {
                    titles.Add(new(
                        title.Id,
                        title.ExternalId,
                        title.Type,
                        new(
                            title.Metadata.Name,
                            new(title.Metadata.Origin.Country, title.Metadata.Origin.Language),
                            title.Metadata.Description,
                            title.Metadata.SeasonNumber),
                        title.ReleaseDate));
                }

                return Results.Ok(new ListTitlesResponse(titles));
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

                return Results.Created(API_ENDPOINT, new CreateTitleResponse(await sender.Send(cmd)));
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
