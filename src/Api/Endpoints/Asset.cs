using Mediaspot.Api.DTOs.Assets;
using Mediaspot.Application.Assets.Commands.Archive;
using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Assets.Commands.RegisterMediaFile;
using Mediaspot.Application.Assets.Commands.UpdateMetadata;
using Mediaspot.Application.Assets.Queries.GetById;
using MediatR;

namespace Mediaspot.Api.Endpoints;

public static class Asset
{
    private const string API_ENDPOINT = "/assets";

    public static WebApplication MapAssetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(API_ENDPOINT).WithTags("Assets");

        group.MapGet("{id:guid}", async (Guid id, ISender sender) =>
            {
                var asset = await sender.Send(new GetAssetByIdQuery(id));
                return Results.Ok(asset);
            })
            .WithName("GetAssetById")
            .WithOpenApi();

        group.MapPost("", async (CreateAssetDto request, ISender sender) =>
            {
                var cmd = new CreateAssetCommand(
                    request.ExternalId,
                    request.Title,
                    request.Description,
                    request.Language);

                return Results.Created(API_ENDPOINT, new { id = await sender.Send(cmd) });
            })
            .WithName("PostCreateAsset")
            .WithOpenApi();

        group.MapPost("{id:guid}/files", async (Guid id, [AsParameters] RegisterMediaFileDto request, ISender sender) =>
            {
                var cmd = new RegisterMediaFileCommand(id, request.Path, request.DurationSeconds);

                return Results.Ok(new { mediaFileId = await sender.Send(cmd) });
            })
            .WithName("PostRegisterMediaFile")
            .WithOpenApi();

        group.MapPut("{id:guid}/metadata", async (Guid id, UpdateAssetMetadataDto dto, ISender sender) =>
            {
                var cmd = new UpdateAssetMetadataCommand(id, dto.Title, dto.Description, dto.Language);

                await sender.Send(cmd);
                return Results.NoContent();
            })
            .WithName("PutUpdateAssetMetadata")
            .WithOpenApi();

        group.MapPost("{id:guid}/archive", async (Guid id, ISender sender) =>
            {
                await sender.Send(new ArchiveAssetCommand(id));
                return Results.NoContent();
            })
            .WithName("PostArchiveAsset")
            .WithOpenApi();

        return app;
    }
}
