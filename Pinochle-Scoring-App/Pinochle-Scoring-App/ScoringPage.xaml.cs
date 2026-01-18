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
        if (!string.IsNullOrEmpty(Team1MeldPoints.Text) && int.TryParse(Team1MeldPoints.Text, out int meldAmount))
        {
            game.Team1.Meld += meldAmount;
            Team1MeldConfirmLabel.Text = $"Added {meldAmount} meld points";
            Team1MeldConfirmLabel.IsVisible = true;
            Team1MeldPoints.Text = string.Empty;
        }
    }

    public void Team2AddMeld(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team2MeldPoints.Text) && int.TryParse(Team2MeldPoints.Text, out int meldAmount))
        {
            game.Team2.Meld += meldAmount;
            Team2MeldConfirmLabel.Text = $"Added {meldAmount} meld points";
            Team2MeldConfirmLabel.IsVisible = true;
            Team2MeldPoints.Text = string.Empty;
        }
    }

    public void OnStartTrickScoring(object sender, EventArgs e)
    {
        Melding.IsVisible = false;
        TrickScoring.IsVisible = true;
    }

    public void Team1AddTrickPoints(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team1TrickPoints.Text) && int.TryParse(Team1TrickPoints.Text, out int trickPoints))
        {
            game.Team1.TrickPoints += trickPoints;

            Team1TrickConfirmLabel.Text = $"Added {trickPoints} trick points";
            Team1TrickConfirmLabel.IsVisible = true;
            Team1TrickPoints.Text = string.Empty;

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
        if (!string.IsNullOrEmpty(Team2TrickPoints.Text) && int.TryParse(Team2TrickPoints.Text, out int trickPoints))
        {
            game.Team2.TrickPoints += trickPoints;

            Team2TrickConfirmLabel.Text = $"Added {trickPoints} trick points";
            Team2TrickConfirmLabel.IsVisible = true;
            Team2TrickPoints.Text = string.Empty;

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
        Team1MeldConfirmLabel.IsVisible = false;
        Team2MeldConfirmLabel.IsVisible = false;
        Team1TrickConfirmLabel.IsVisible = false;
        Team2TrickConfirmLabel.IsVisible = false;
        Bidding.IsVisible = true;
        Melding.IsVisible = false;
        TrickScoring.IsVisible = false;
    }

    private void OnBidAmountChanged(object sender, TextChangedEventArgs e)
    {
        bool hasValidBid = !string.IsNullOrWhiteSpace(e.NewTextValue) && 
                           int.TryParse(e.NewTextValue, out int bidValue) && 
                           bidValue > 0;
        
        Team1BidButton.IsVisible = hasValidBid;
        Team2BidButton.IsVisible = hasValidBid;
        BidPromptLabel.IsVisible = hasValidBid;
    }

    private void OnNumericEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue) && !int.TryParse(e.NewTextValue, out _))
            {
                entry.Text = e.OldTextValue;
            }
        }
    }

}