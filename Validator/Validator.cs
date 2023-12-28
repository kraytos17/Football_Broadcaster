using FluentValidation;
using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Validator;

public sealed class PlayerRequestValidator : AbstractValidator<Player> {
    private readonly IConfiguration _configuration;

    public PlayerRequestValidator(IConfiguration configuration) {
        _configuration = configuration;

        string[] branches = _configuration["BranchNames"].Split(",");
        string[] positions = _configuration["FootballPos"].Split(",");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$");
        RuleFor(x => x.Assists)
            .Cascade(CascadeMode.Stop)
            .NotEmpty();
        RuleFor(x => x.Branch)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => branches.Contains(x));
        RuleFor(x => x.Position)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(p => positions.Contains(p));
        RuleFor(x => x.Year)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(2020);
        //RuleFor(x => x.Instagram)
        //	.Cascade(CascadeMode.Stop)
        //	.NotEmpty()
        //	.Matches("^(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?\\/[a-zA-Z0-9]{2,}|((https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?)|(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}(\\.[a-zA-Z0-9]{2,})?");
        RuleFor(x => x.Age)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(18)
            .LessThanOrEqualTo(26);
        RuleFor(x => x.CollegeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches("^B[0-9]{6}$");
        RuleFor(x => x.Goals)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}

public sealed class TeamRequestValidator : AbstractValidator<Team> {
    private readonly IConfiguration _config;

    public TeamRequestValidator(IConfiguration config) {
        _config = config;

        int teamCount = _config["TeamNames"].Split(",").Length;

        RuleFor(x => x.GamesPlayed)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.GoalDifference)
            .Cascade(CascadeMode.Stop);
        RuleFor(x => x.LeaguePosition)
            .Cascade(CascadeMode.Stop)
            .InclusiveBetween(1, teamCount);
        RuleFor(x => x.MatchesLost)
            .Cascade(CascadeMode.Stop);
        RuleFor(x => x.MatchesWon)
            .Cascade(CascadeMode.Stop);
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z1-9_]+$");
        RuleFor(x => x.Points)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.TeamId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}
