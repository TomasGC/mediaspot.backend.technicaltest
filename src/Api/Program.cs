using Mediaspot.Api.DTOs.Assets;
using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Api.Endpoints;
using Mediaspot.Application.Assets.Commands.Archive;
using Mediaspot.Application.Assets.Commands.Create;
using Mediaspot.Application.Assets.Commands.RegisterMediaFile;
using Mediaspot.Application.Assets.Commands.UpdateMetadata;
using Mediaspot.Application.Assets.Queries.GetById;
using Mediaspot.Application.Titles.Commands.Create;
using Mediaspot.Application.Titles.Commands.UpdateMetadata;
using Mediaspot.Application.Titles.Queries.GetByExternalId;
using Mediaspot.Application.Titles.Queries.GetById;
using Mediaspot.Application.Titles.Queries.List;
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

app.MapTitleEndpoints()
    .MapAssetEndpoints();

app.Run();
