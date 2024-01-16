using FluentValidation;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Endpoints;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/team");
        group.MapGet("{name}", GetTeamByName);
        group.MapPost("/", CreateTeam);
        group.MapPut("/", UpdateTeam);
        group.MapDelete("{teamId:int}", DeleteTeam);
        group.MapGet("{teamId:int}", GetTeamByTeamId);
        app.MapGet("api/teams", GetAllTeams);
    }

    public static async Task<IResult> CreateTeam(Team team, ITeamRepo teamRepo, IValidator<Team> teamRequestValidator, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            var res = await teamRequestValidator.ValidateAsync(team, ct);
            if (!res.IsValid)
            {
                return TypedResults.BadRequest("Validation error, invalid team data.");
            }

            var created = await teamRepo.CreateTeam(team, ct);
            if (!created)
            {
                return TypedResults.StatusCode(500);
            }

            return TypedResults.Created();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }

    public static async Task<IResult> GetTeamByName(string name, ITeamRepo teamRepo, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return TypedResults.BadRequest(
                    "Name param cannot be empty or just whitespaces.");
            }

            var team = await teamRepo.GetTeam(name, ct);
            if (team is null)
            {
                throw new NullReferenceException("Team object reference is null");
            }

            if (string.IsNullOrEmpty(team.Id) || string.IsNullOrWhiteSpace(team.Id))
            {
                return TypedResults.NotFound(name);
            }

            return TypedResults.Ok(team);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }

    public static async Task<IResult> GetTeamByTeamId(int teamId, ITeamRepo teamRepo, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            if (teamId <= 0)
            {
                return TypedResults.BadRequest("Team Id cannot be zero or negative.");
            }

            var team = await teamRepo.GetTeam(teamId, ct)
                       ?? throw new NullReferenceException(
                           "Team object reference is null.");
            return TypedResults.Ok(team);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }

    public static async Task<IResult> UpdateTeam(Team team, ITeamRepo teamRepo, IValidator<Team> teamRequestValidator, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            var res = await teamRequestValidator.ValidateAsync(team, ct);
            if (!res.IsValid)
            {
                return TypedResults.BadRequest("Validation error, invalid team data.");
            }

            var updated = await teamRepo.UpdateTeam(team, ct);
            if (!updated)
            {
                return TypedResults.StatusCode(500);
            }

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }

    public static async Task<IResult> DeleteTeam(int teamId, ITeamRepo teamRepo, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            if (teamId <= 0)
            {
                return TypedResults.BadRequest("TeamId cannot be zero or negative.");
            }

            var deleted = await teamRepo.DeleteTeam(teamId, ct);
            if (!deleted)
            {
                return TypedResults.StatusCode(500);
            }

            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }

    public static async Task<IResult> GetAllTeams(ITeamRepo teamRepo, CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            var res = await teamRepo.GetAllTeams(ct)
                      ?? throw new NullReferenceException(
                          "Team enumerable has null reference.");
            return TypedResults.Ok(res);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return TypedResults.StatusCode(500);
        }
    }
}
