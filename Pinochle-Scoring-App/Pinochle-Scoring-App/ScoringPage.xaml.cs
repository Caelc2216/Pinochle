namespace Pinochle_Scoring_App;

public partial class ScoringPage : ContentPage
{
    private Game game;

    public ScoringPage(Game game)
    {
        InitializeComponent();
        this.game = game;
        BindingContext = this.game;
    }

    public void OnTeam1Bid(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(BidAmount.Text))
        {
            int bidAmount = int.Parse(BidAmount.Text);
            game.Team1.Bid = bidAmount;
            DisplayBidResult(game.Team1.Name, bidAmount);
        }
    }

    public void OnTeam2Bid(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(BidAmount.Text))
        {
            int bidAmount = int.Parse(BidAmount.Text);
            game.Team2.Bid = bidAmount;
            DisplayBidResult(game.Team2.Name, bidAmount);
        }
    }

    private void DisplayBidResult(string teamName, int bidAmount)
    {
        BidResultLabel.Text = $"{teamName} took the bid for {bidAmount}";
        BidResultLabel.IsVisible = true;
        Bidding.IsVisible = false;
        Melding.IsVisible = true;
    }

    public void Team1AddMeld(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team1MeldPoints.Text))
        {
            int meldAmount = int.Parse(Team1MeldPoints.Text);
            game.Team1.Meld += meldAmount;
        }
    }

    public void Team2AddMeld(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team2MeldPoints.Text))
        {
            int meldAmount = int.Parse(Team2MeldPoints.Text);
            game.Team2.Meld += meldAmount;
        }
    }

    public void OnStartTrickScoring(object sender, EventArgs e)
    {
        Melding.IsVisible = false;
        TrickScoring.IsVisible = true;
    }

    public void Team1AddTrickPoints(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team1TrickPoints.Text))
        {
            int trickPoints = int.Parse(Team1TrickPoints.Text);
            game.Team1.TrickPoints += trickPoints;

            if(game.Team1.Bid != null && game.Team1.TrickPoints < game.Team1.Bid)
            {
                // Team 1 failed to meet their bid
                game.Team1.Score -= game.Team1.Bid;
            }
            else
            {
                // Team 1 met or exceeded their bid
                game.Team1.Score += game.Team1.TrickPoints + game.Team1.Meld;
            }
        }

    }

    public void Team2AddTrickPoints(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team2TrickPoints.Text))
        {
            int trickPoints = int.Parse(Team2TrickPoints.Text);
            game.Team2.TrickPoints += trickPoints;

            if (game.Team2.Bid != null && game.Team2.TrickPoints < game.Team2.Bid)
            {
                // Team 2 failed to meet their bid
                game.Team2.Score -= game.Team2.Bid;
            }
            else
            {
                // Team 2 met or exceeded their bid
                game.Team2.Score += game.Team2.TrickPoints + game.Team2.Meld;
            }
        }
    }

    public void NextRound(object sender, EventArgs e)
    {
        game.CheckForWinner();

        if (game.Winner != null)
        {
            // Navigate to WinnerPage
            Navigation.PushAsync(new WinnerPage(game.Winner));
            return;
        }

        // Reset for next round
        game.Team1.Bid = 0;
        game.Team1.Meld = 0;
        game.Team1.TrickPoints = 0;
        game.Team2.Bid = 0;
        game.Team2.Meld = 0;
        game.Team2.TrickPoints = 0;
        BidAmount.Text = string.Empty;
        Team1MeldPoints.Text = string.Empty;
        Team2MeldPoints.Text = string.Empty;
        Team1TrickPoints.Text = string.Empty;
        Team2TrickPoints.Text = string.Empty;
        BidResultLabel.IsVisible = false;
        Bidding.IsVisible = true;
        Melding.IsVisible = false;
        TrickScoring.IsVisible = false;
    }


}