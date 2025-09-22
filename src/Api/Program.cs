using Mediaspot.Api.Endpoints;
using Mediaspot.Application.Workers;
using Mediaspot.Infrastructure;
using Mediaspot.Infrastructure.Persistence;
using Mediaspot.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure("Mediaspot.Backend.TechnicalTest");
builder.Services.AddHostedService<TranscoderBackgroundService>();
builder.Services.AddSingleton<ITranscodeJobQueue, TranscodeJobQueue>();

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
