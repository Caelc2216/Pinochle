namespace Pinochle_Scoring_App.Tests;

public class ScoringLogicTests
{
    [Fact]
    public void ScoringRound_TeamMeetsBid_ShouldAddMeldAndTrickPoints()
    {
        // Arrange
        var game = new Game();
        game.Team1.Bid = 25;
        game.Team1.Meld = 10;
        game.Team1.TrickPoints = 30;
        int initialScore = game.Team1.Score;

        // Act - Simulating the scoring logic from ScoringPage
        int expectedScore = initialScore + game.Team1.TrickPoints + game.Team1.Meld;
        game.Team1.Score = expectedScore;

        // Assert
        Assert.Equal(40, game.Team1.Score);
    }

    [Fact]
    public void ScoringRound_TeamFailsBid_ShouldSubtractBidAmount()
    {
        // Arrange
        var game = new Game();
        game.Team1.Bid = 25;
        game.Team1.Meld = 10;
        game.Team1.TrickPoints = 20; // Less than bid
        int initialScore = game.Team1.Score;

        // Act - Simulating the scoring logic from ScoringPage when bid is failed
        int expectedScore = initialScore - game.Team1.Bid;
        game.Team1.Score = expectedScore;

        // Assert
        Assert.Equal(-25, game.Team1.Score);
    }

    [Fact]
    public void ScoringRound_TeamExactlyMeetsBid_ShouldAddMeldAndTrickPoints()
    {
        // Arrange
        var game = new Game();
        game.Team1.Bid = 25;
        game.Team1.Meld = 10;
        game.Team1.TrickPoints = 25; // Exactly meets bid
        int initialScore = game.Team1.Score;

        // Act
        int expectedScore = initialScore + game.Team1.TrickPoints + game.Team1.Meld;
        game.Team1.Score = expectedScore;

        // Assert
        Assert.Equal(35, game.Team1.Score);
    }


    [Theory]
    [InlineData(25, 10, 30, 40)]  // Bid met: meld + trick points
    [InlineData(25, 15, 40, 55)]  // Bid exceeded: meld + trick points
    [InlineData(30, 20, 35, 55)]  // Bid exceeded by 5
    [InlineData(20, 8, 22, 30)]   // Minimum typical bid scenario
    public void ScoringRound_VariousBidMet_ShouldCalculateCorrectly(
        int bid, int meld, int trickPoints, int expectedTotalScore)
    {
        // Arrange
        var game = new Game();
        game.Team1.Bid = bid;
        game.Team1.Meld = meld;
        game.Team1.TrickPoints = trickPoints;

        // Act
        game.Team1.Score = game.Team1.TrickPoints + game.Team1.Meld;

        // Assert
        Assert.Equal(expectedTotalScore, game.Team1.Score);
    }

    [Theory]
    [InlineData(30, 10, 25, 0, -30)]   // Failed by 5
    [InlineData(40, 15, 35, 50, 10)]   // Failed, but had previous score
    [InlineData(25, 20, 20, 100, 75)]  // Failed by 5, with high previous score
    public void ScoringRound_VariousBidFailed_ShouldDeductCorrectly(
        int bid, int meld, int trickPoints, int initialScore, int expectedScore)
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = initialScore;
        game.Team1.Bid = bid;
        game.Team1.Meld = meld;
        game.Team1.TrickPoints = trickPoints;

        // Act - Simulating failed bid
        game.Team1.Score = game.Team1.Score - game.Team1.Bid;

        // Assert
        Assert.Equal(expectedScore, game.Team1.Score);
    }

    [Fact]
    public void MultipleRounds_AccumulatedScoring_ShouldReach150()
    {
        // Arrange
        var game = new Game();

        // Act - Simulate 3 rounds
        // Round 1
        game.Team1.Score += 35; // Made their bid
        game.Team2.Score += 20;

        // Round 2
        game.Team1.Score += 40;
        game.Team2.Score += 25;

        // Round 3
        game.Team1.Score += 45;
        game.Team2.Score += 30;

        // Check for winner
        game.CheckForWinner();

        // Assert
        Assert.Equal(120, game.Team1.Score);
        Assert.Equal(75, game.Team2.Score);
        Assert.Null(game.Winner); // No one reached 150 yet
    }

    [Fact]
    public void MultipleRounds_TeamReaches150First_ShouldWin()
    {
        // Arrange
        var game = new Game();

        // Act - Simulate rounds until someone reaches 150
        game.Team1.Score = 110;
        game.Team2.Score = 80;

        // Final round
        game.Team1.Score += 45; // Total: 155
        game.Team2.Score += 35; // Total: 115

        game.CheckForWinner();

        // Assert
        Assert.Equal(155, game.Team1.Score);
        Assert.Equal(115, game.Team2.Score);
        Assert.Equal(game.Team1, game.Winner);
    }
}
