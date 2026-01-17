namespace Pinochle_Scoring_App;

public partial class WinnerPage : ContentPage
{
	private Team winner;
    public WinnerPage(Team winner)
    {
        InitializeComponent();
        this.winner = winner;
        BindingContext = this.winner;
    }

    private async void OnStartNewGameButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeamEntryPage());
    }

}