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
            Melding.IsVisible = true;
        }
    }

    public void OnTeam2Bid(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(BidAmount.Text))
        {
            int bidAmount = int.Parse(BidAmount.Text);
            game.Team2.Bid = bidAmount;
            DisplayBidResult(game.Team2.Name, bidAmount);
            Melding.IsVisible = true;
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
            if (meldAmount != game.Team1.Meld)
            {
                game.Team1.Meld += meldAmount;
                Team1MeldEntry.IsVisible = false;
            }
        }
    }

    public void Team2AddMeld(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Team2MeldPoints.Text) && int.TryParse(Team2MeldPoints.Text, out int meldAmount))
        {
            if (meldAmount != game.Team2.Meld)
            {
                game.Team2.Meld += meldAmount;
                Team2MeldEntry.IsVisible = false;
            }
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
            if(game.Team1.TrickPoints == 0)
            {
                game.Team1.TrickPoints += trickPoints;
                Team1TrickEntry.IsVisible = false;
            }

            if(game.Team1.Bid != 0 && (game.Team1.TrickPoints + game.Team1.Meld) < game.Team1.Bid)
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
            if(game.Team2.TrickPoints == 0)
            {
                game.Team2.TrickPoints += trickPoints;
                Team2TrickEntry.IsVisible = false;
            }

            if (game.Team2.Bid != 0 && (game.Team2.TrickPoints + game.Team2.Meld) < game.Team2.Bid)
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
        TrickPointsWarningLabel.IsVisible = false;
        Team1MeldEntry.IsVisible = true;  // Add this
        Team2MeldEntry.IsVisible = true;  // Add this
        Team1TrickEntry.IsVisible = true;  // Add this
        Team2TrickEntry.IsVisible = true;  // Add this
        ContinueToTrickScoringButton.IsVisible = false;  // Add this
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

    private void OnMeldPointsChanged(object sender, TextChangedEventArgs e)
    {
        bool bothEntriesFilled = !string.IsNullOrWhiteSpace(Team1MeldPoints.Text) &&
                                 !string.IsNullOrWhiteSpace(Team2MeldPoints.Text);

        ContinueToTrickScoringButton.IsVisible = bothEntriesFilled;
    }

    private void OnTrickPointsChanged(object sender, TextChangedEventArgs e)
    {
        bool bothEntriesFilled = !string.IsNullOrWhiteSpace(Team1TrickPoints.Text) &&
                                 !string.IsNullOrWhiteSpace(Team2TrickPoints.Text);

        if (bothEntriesFilled)
        {
            // Parse the trick points and check if they add up to 25
            if (int.TryParse(Team1TrickPoints.Text, out int team1Tricks) &&
                int.TryParse(Team2TrickPoints.Text, out int team2Tricks))
            {
                int totalTricks = team1Tricks + team2Tricks;
                
                if (totalTricks == 25)
                {
                    // Valid total - show buttons and Next Round button, hide warning
                    Team1AddTrickButton.IsVisible = true;
                    Team2AddTrickButton.IsVisible = true;
                    NextRoundButton.IsVisible = true;
                    TrickPointsWarningLabel.IsVisible = false;
                }
                else
                {
                    // Invalid total - hide buttons and show warning
                    Team1AddTrickButton.IsVisible = false;
                    Team2AddTrickButton.IsVisible = false;
                    NextRoundButton.IsVisible = false;
                    TrickPointsWarningLabel.IsVisible = true;
                }
            }
            else
            {
                // Invalid input - hide everything
                Team1AddTrickButton.IsVisible = false;
                Team2AddTrickButton.IsVisible = false;
                NextRoundButton.IsVisible = false;
                TrickPointsWarningLabel.IsVisible = false;
            }
        }
        else
        {
            // Not both entries filled - hide everything
            Team1AddTrickButton.IsVisible = false;
            Team2AddTrickButton.IsVisible = false;
            NextRoundButton.IsVisible = false;
            TrickPointsWarningLabel.IsVisible = false;
        }
    }

}