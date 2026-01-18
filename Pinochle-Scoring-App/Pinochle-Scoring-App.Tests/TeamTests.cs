namespace Pinochle_Scoring_App.Tests;

public class TeamTests
{
    [Fact]
    public void Team_InitialScore_ShouldBeZero()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.Equal(0, team.Score);
    }

    [Fact]
    public void Team_InitialBid_ShouldBeZero()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.Equal(0, team.Bid);
    }

    [Fact]
    public void Team_InitialMeld_ShouldBeZero()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.Equal(0, team.Meld);
    }

    [Fact]
    public void Team_InitialTrickPoints_ShouldBeZero()
    {
        // Arrange & Act
        var team = new Team();

        // Assert
        Assert.Equal(0, team.TrickPoints);
    }
}
