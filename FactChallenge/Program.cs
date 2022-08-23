using FactChallenge.Infrastructure.Core.DataSource;
using FactChallenge.Infrastructure.Core.Interfaces;
using FactChallenge.Infrastructure.Core.Services;
using FactChallenge.Infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = $"{builder.Environment.ApplicationName} v1", Version = "v1" });
    options.SwaggerDoc("v2", new() { Title = $"{builder.Environment.ApplicationName} v2", Version = "v2" });
});
builder.Services.AddScoped<IFactService, FactService>();
builder.Services.AddScoped<IDataPath, DataPath>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
        options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
    });
}

// API to create facts 
app.MapPost("/fact", async (IFactService factService, CatFactModel catFactRequest) =>
{
    if (catFactRequest == null)
        return Results.BadRequest("Bad request.Invalid fact.");

   // Log.Information("Request payload" + catFactRequest);

    var response = await factService.CreateFact(catFactRequest);

   // Log.Information("Response payload" + response);
    return response != null ? Results.Created($"/fact/{response.Length}", response) : Results.BadRequest("Bad request.Invalid fact.");
})
.WithName("Add new cats")
.WithTags("Fact")
.WithMetadata("Get Random Facr")
    .Produces<CatFactModel>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);

// API to get random fact
app.MapGet("/fact", async (IFactService factService, int max_length) =>
{
    var response = await factService.GetRandomFacts(max_length);

    return response != null ? Results.Ok(response) : Results.NotFound();
})
.WithName("A fact about cats")
.WithTags("Fact")
    .Produces<CatFactModel>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);


// API to get list of facts
app.MapGet("/facts", async (IFactService factService, int? max_length, int? limit) =>
{
    int requestLimit = limit ?? 1;
    int requestMaxLen = max_length ?? 0;

    var response = await factService.GetFacts(requestMaxLen, requestLimit);

    return response != null ? Results.Ok(response) : Results.NotFound();
})
.WithName("Facts about cats")
.WithTags("Facts")
    .Produces<CatFactModel>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);

app.Run();

