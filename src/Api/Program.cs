using Mediaspot.Api.DTOs;
using Mediaspot.Application.Assets.Commands.Archive;
using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Assets.Commands.RegisterMediaFile;
using Mediaspot.Application.Assets.Commands.UpdateMetadata;
using Mediaspot.Application.Assets.Queries.GetById;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Queries.GetByExternalId;
using Mediaspot.Application.Titles.Queries.GetById;
using Mediaspot.Infrastructure;
using Mediaspot.Infrastructure.Persistence;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure("Mediaspot.Backend.TechnicalTest");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MediaspotDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/titles")
    .MapGet("{id:guid}", async (Guid id, ISender sender) =>
    {
        var asset = await sender.Send(new GetTitleByIdQuery(id));
        return Results.Ok(asset);
    })
    .WithName("GetTitleById")
    .WithTags("Titles")
    .WithOpenApi();

app.MapGroup("/titles")
    .MapGet("{externalId}", async (string externalId, ISender sender) =>
    {
        var asset = await sender.Send(new GetTitleByExternalIdQuery(externalId));
        return Results.Ok(asset);
    })
    .WithName("GetTitleByExternalId")
    .WithTags("Titles")
    .WithOpenApi();

app.MapGroup("/titles")
    .MapPost("", async (CreateTitleCommand cmd, ISender sender) =>
    {
        Results.Created("/titles", new { id = await sender.Send(cmd) });
    })
    .WithName("PostCreateTitle")
    .WithTags("Titles")
    .WithOpenApi();



app.MapGroup("/assets")
    .MapGet("{id:guid}", async (Guid id, ISender sender) =>
    {
        var asset = await sender.Send(new GetAssetByIdQuery(id));
        return Results.Ok(asset);
    })
    .WithName("GetAssetById")
    .WithTags("Assets")
    .WithOpenApi();

app.MapGroup("/assets")
    .MapPost("", async (CreateAssetCommand cmd, ISender sender) =>
    {
        Results.Created("/assets", new { id = await sender.Send(cmd) });
    })
    .WithName("PostCreateAsset")
    .WithTags("Assets")
    .WithOpenApi();

app.MapGroup("/assets")
    .MapPost("{id:guid}/files", async (Guid id, string path, double durationSeconds, ISender sender) =>
    {
        Results.Ok(new { mediaFileId = await sender.Send(new RegisterMediaFileCommand(id, path, durationSeconds)) });
    })
    .WithName("PostRegisterMediaFile")
    .WithTags("Assets")
    .WithOpenApi();

app.MapGroup("/assets")
    .MapPut("{id:guid}/metadata", async (Guid id, UpdateMetadataDto dto, ISender sender) =>
    {
        await sender.Send(new UpdateMetadataCommand(id, dto.Title, dto.Description, dto.Language));
        return Results.NoContent();
    })
    .WithName("PutUpdateMetadata")
    .WithTags("Assets")
    .WithOpenApi();

app.MapGroup("/assets")
    .MapPost("{id:guid}/archive", async (Guid id, ISender sender) =>
    {
        await sender.Send(new ArchiveAssetCommand(id));
        return Results.NoContent();
    })
    .WithName("PostArchiveAsset")
    .WithTags("Assets")
    .WithOpenApi();

app.Run();
