using API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPlaysRepository, PlaysRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseStatusCodePages();
app.UseExceptionHandler();


app.MapGet("/plays", async (IPlaysRepository repository) =>
{
    var plays = await repository.GetAllAsync();
    return Results.Ok(plays);
});

app.MapGet("/plays/{id}", async (IPlaysRepository repository, int id) =>
{
    var play = await repository.GetAsync(id);
    return play is not null ? Results.Ok(play) : Results.NotFound();
});

app.Run();
