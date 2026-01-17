using System.ComponentModel;

namespace Pinochle_Scoring_App;

public partial class TeamEntryPage : ContentPage, INotifyPropertyChanged
{
	private string team1Name = string.Empty;
	public string Team1Name
	{
		get => team1Name;
		set
		{
			if (team1Name != value)
			{
				team1Name = value;
				OnPropertyChanged();
			}
		}
	}

	private string team2Name = string.Empty;
	public string Team2Name
	{
		get => team2Name;
		set
		{
			if (team2Name != value)
			{
				team2Name = value;
				OnPropertyChanged();
			}
		}
	}

	private string displayedTeam1Name = string.Empty;
	public string DisplayedTeam1Name
	{
		get => displayedTeam1Name;
		set
		{
			if (displayedTeam1Name != value)
			{
				displayedTeam1Name = value;
				OnPropertyChanged();
			}
		}
	}

	private string displayedTeam2Name = string.Empty;
	public string DisplayedTeam2Name
	{
		get => displayedTeam2Name;
		set
		{
			if (displayedTeam2Name != value)
			{
				displayedTeam2Name = value;
				OnPropertyChanged();
			}
		}
	}

	public Game game = new Game();

	public TeamEntryPage()
	{
		InitializeComponent();
		BindingContext = this;
		
		// Initialize teams
		game.Team1 = new Team();
		game.Team2 = new Team();
	}

	public void SetTeam1Name(object sender, EventArgs e)
	{
		game.Team1.Name = Team1Name;
		DisplayedTeam1Name = Team1Name; // Update display property
	}

	public void SetTeam2Name(object sender, EventArgs e)
	{
		game.Team2.Name = Team2Name;
		DisplayedTeam2Name = Team2Name; // Update display property
	}

	private async void OnStartScoring(object sender, EventArgs e)
	{
		// Set team names before navigating
		game.Team1.Name = Team1Name;
		game.Team2.Name = Team2Name;
		
		await Navigation.PushAsync(new ScoringPage(game));
	}
}