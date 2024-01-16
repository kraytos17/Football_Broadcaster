using FluentValidation;
using Gc_Broadcasting_Api.Models;
using static FluentValidation.CascadeMode;

namespace Gc_Broadcasting_Api.Validator;

public sealed class PlayerRequestValidator : AbstractValidator<Player> {
    public PlayerRequestValidator(IConfiguration configuration) {
        var branches = configuration["BranchNames"]?.Split(",");
        var positions = configuration["FootballPos"]?.Split(",");

        RuleFor(x => x.Name)
            .Cascade(Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$");
        RuleFor(x => x.Assists)
            .Cascade(Stop)
            .NotEmpty();
        RuleFor(x => x.Branch)
            .Cascade(Stop)
            .NotEmpty()
            .Must(x => branches != null && branches.Contains(x));
        RuleFor(x => x.Position)
            .Cascade(Stop)
            .NotEmpty()
            .Must(p => positions != null && positions.Contains(p));
        RuleFor(x => x.Year)
            .Cascade(Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(2020);
        //RuleFor(x => x.Instagram)
        //	.Cascade(CascadeMode.Stop)
        //	.NotEmpty()
        //	.Matches("^(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?\\/[a-zA-Z0-9]{2,}|((https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?)|(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}(\\.[a-zA-Z0-9]{2,})?");
        RuleFor(x => x.Age)
            .Cascade(Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(18)
            .LessThanOrEqualTo(26);
        RuleFor(x => x.CollegeId)
            .Cascade(Stop)
            .NotEmpty()
            .Matches("^B[0-9]{6}$");
        RuleFor(x => x.Goals)
            .Cascade(Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamId)
            .Cascade(Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
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
