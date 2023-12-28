using FluentValidation;
using Gc_Broadcasting_Api.Endpoints;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Gc_Broadcasting_Api.Repository;
using Gc_Broadcasting_Api.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddScoped<IValidator<Player>, PlayerRequestValidator>();
builder.Services.AddScoped<IValidator<Team>, TeamRequestValidator>();
builder.Services.AddSingleton<IPlayerRepo, PlayerRepository>();
builder.Services.AddSingleton<ITeamRepo, TeamRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
PlayerEndpoints.MapPlayerEndpoints(app);
TeamEndpoints.MapTeamEndpoints(app);
app.Run();
