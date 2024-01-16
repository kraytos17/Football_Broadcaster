using FluentValidation;
using Gc_Broadcasting_Api.Endpoints;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;
using Gc_Broadcasting_Api.Repository;
using Gc_Broadcasting_Api.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDb"));
//builder.Services.AddScoped<DatabaseSettings>();
builder.Services.AddScoped<IValidator<Player>, PlayerRequestValidator>();
builder.Services.AddSingleton<IValidator<Team>, TeamRequestValidator>();
builder.Services.AddSingleton<IPlayerRepo, PlayerRepository>();
builder.Services.AddSingleton<ITeamRepo, TeamRepository>();
builder.Services.AddSingleton<IAdminRepo, AdminRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPlayerEndpoints();
app.MapTeamEndpoints();
app.MapAdminEndpoints();
app.Run();
