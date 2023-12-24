using FluentValidation;
using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Validator;

public class PlayerValidator : AbstractValidator<Player> {
    private readonly IPlayerRepo _playerRepo;
    private readonly IConfiguration _configuration;

    public PlayerValidator(IPlayerRepo playerRepo, IConfiguration configuration) {
		_playerRepo = playerRepo;
		_configuration = configuration;

        string[] branches = _configuration["BranchNames"].Split(",");
        string[] positions = _configuration["FootballPos"].Split(",");

        RuleFor(x => x.Id)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.MinimumLength(24)
			.MaximumLength(24)
			.Matches("^[a-fA-F0-9]{24}$");
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
		RuleFor(x => x.Instagram)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.Matches("^(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?\\/[a-zA-Z0-9]{2,}|((https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?)|(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}(\\.[a-zA-Z0-9]{2,})?");
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
		RuleFor(x => x.Imagelink)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.Matches("^(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?\\/[a-zA-Z0-9]{2,}|((https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})(\\.[a-zA-Z]{2,})?)|(https:\\/\\/www\\.|http:\\/\\/www\\.|https:\\/\\/|http:\\/\\/)?[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}(\\.[a-zA-Z0-9]{2,})?");
		RuleFor(x => x.TeamId)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.GreaterThanOrEqualTo(1);
	}
}

public sealed class TeamValidator : AbstractValidator<Team> {
	private readonly ITeamRepo _teamRepo;
	private readonly IConfiguration _config;
		
    public TeamValidator(ITeamRepo teamRepo, IConfiguration config) {
        _config = config;
		_teamRepo = teamRepo;

        int teamCount = _config["TeamNames"].Split(",").Length;

        RuleFor(x => x.GamesPlayed)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.GreaterThanOrEqualTo(0);
		RuleFor(x => x.GoalDifference)
			.Cascade(CascadeMode.Stop)
			.NotEmpty();
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
			.MinimumLength(24)
            .MaximumLength(24)
            .Matches("^[a-fA-F0-9]{24}$");
		RuleFor(x => x.LeaguePosition)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.InclusiveBetween(1, teamCount);
		RuleFor(x => x.MatchesLost)
			.Cascade(CascadeMode.Stop)
			.NotEmpty();
		RuleFor(x => x.MatchesWon)
			.Cascade(CascadeMode.Stop)
			.NotEmpty();
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(100)
            .Matches("^[a-zA-Z1-9]+$");
		RuleFor(x => x.Points)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.GreaterThanOrEqualTo(0);
		RuleFor(x => x.TeamId)
			.Cascade(CascadeMode.Stop)
			.NotEmpty()
			.GreaterThanOrEqualTo(1);
    }
}
