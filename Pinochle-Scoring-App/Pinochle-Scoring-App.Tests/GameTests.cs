namespace Pinochle_Scoring_App.Tests;

public class GameTests
{
    [Fact]
    public void Game_Constructor_ShouldInitializeTeams()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        Assert.NotNull(game.Team1);
        Assert.NotNull(game.Team2);
    }

    [Fact]
    public void Game_InitialWinner_ShouldBeNull()
    {
        // Arrange & Act
        var game = new Game();

        // Assert
        Assert.Null(game.Winner);
    }

    [Fact]
    public void AddScore_Team1_ShouldIncreaseTeam1Score()
    {
        // Arrange
        var game = new Game();
        int initialScore = game.Team1.Score;
        int scoreToAdd = 20;

        // Act
        game.AddScore(1, scoreToAdd);

        // Assert
        Assert.Equal(initialScore + scoreToAdd, game.Team1.Score);
    }

    [Fact]
    public void AddScore_Team2_ShouldIncreaseTeam2Score()
    {
        // Arrange
        var game = new Game();
        int initialScore = game.Team2.Score;
        int scoreToAdd = 30;

        // Act
        game.AddScore(2, scoreToAdd);

        // Assert
        Assert.Equal(initialScore + scoreToAdd, game.Team2.Score);
    }

    [Fact]
    public void CheckForWinner_Team1Reaches150_ShouldSetTeam1AsWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 150;
        game.Team2.Score = 100;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Equal(game.Team1, game.Winner);
    }

    [Fact]
    public void CheckForWinner_Team2Reaches150_ShouldSetTeam2AsWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 100;
        game.Team2.Score = 150;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Equal(game.Team2, game.Winner);
    }

    [Fact]
    public void CheckForWinner_BothTeamsBelow150_ShouldHaveNoWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 100;
        game.Team2.Score = 120;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Null(game.Winner);
    }

    [Fact]
    public void CheckForWinner_BothTeamsAbove150_HigherScoreWins()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 155;
        game.Team2.Score = 160;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Equal(game.Team2, game.Winner);
    }

    [Fact]
    public void CheckForWinner_Team1LeadBy150WithNegativeScore_ShouldSetTeam1AsWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 100;
        game.Team2.Score = -50;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Equal(game.Team1, game.Winner);
    }

    [Fact]
    public void CheckForWinner_Team2LeadBy150WithNegativeScore_ShouldSetTeam2AsWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = -50;
        game.Team2.Score = 100;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Equal(game.Team2, game.Winner);
    }

    [Fact]
    public void CheckForWinner_BothTeamsAt150Tie_ShouldHaveNoWinner()
    {
        // Arrange
        var game = new Game();
        game.Team1.Score = 150;
        game.Team2.Score = 150;

        // Act
        game.CheckForWinner();

        // Assert
        Assert.Null(game.Winner);
    }

    [Fact]
    public void AddScore_MultipleRounds_ShouldAccumulateScores()
    {
        // Arrange
        var game = new Game();

        // Act
        game.AddScore(1, 20);
        game.AddScore(1, 30);
        game.AddScore(2, 15);
        game.AddScore(2, 25);

        // Assert
        Assert.Equal(50, game.Team1.Score);
        Assert.Equal(40, game.Team2.Score);
    }
}
