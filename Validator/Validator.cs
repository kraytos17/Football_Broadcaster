using FluentValidation;
using Gc_Broadcasting_Api.Models;
using static FluentValidation.CascadeMode;

namespace Gc_Broadcasting_Api.Validator;

public sealed class PlayerRequestValidator : AbstractValidator<Player> {
    public PlayerRequestValidator(IConfiguration configuration) {
        var branches = configuration["BranchNames"]?.Split(",");
        var positions = configuration["FootballPos"]?.Split(",");

        RuleFor(x => x.FirstName)
            .Cascade(Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$");
        RuleFor(x => x.LastName)
            .Cascade(Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$");
        RuleFor(x => x.Branch)
            .Cascade(Stop)
            .NotEmpty()
            .Must(x => branches != null && branches.Contains(x));
    }
}

public sealed class TeamRequestValidator : AbstractValidator<Team> {
    public TeamRequestValidator(IConfiguration config) {
        var teamCount = config["TeamNames"]!.Split(",").Length;

        RuleFor(x => x.GamesPlayed)
            .Cascade(Stop)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.GoalDifference)
            .Cascade(Stop);
        RuleFor(x => x.LeaguePosition)
            .Cascade(Stop)
            .InclusiveBetween(1, teamCount);
        RuleFor(x => x.MatchesLost)
            .Cascade(Stop);
        RuleFor(x => x.MatchesWon)
            .Cascade(Stop);
        RuleFor(x => x.Name)
            .Cascade(Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z1-9_]+$");
        RuleFor(x => x.Points)
            .Cascade(Stop)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamId)
            .Cascade(Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
