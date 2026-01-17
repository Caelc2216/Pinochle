using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
    
namespace Pinochle_Scoring_App
{
    public partial class Game : ObservableObject
    {
        [ObservableProperty]
        private Team team1;

        [ObservableProperty]
        private Team team2;

        [ObservableProperty]
        public Team winner;

        public Game()
        {
            team1 = new Team();
            team2 = new Team();
        }

        public void AddScore(int teamNumber, int scoreToAdd)
        {
            if (teamNumber == 1)
            {
                Team1.Score += scoreToAdd;
            }
            else if (teamNumber == 2)
            {
                Team2.Score += scoreToAdd;
            }
        }

        public void CheckForWinner()
        {
            if (Team1.Score >= 150)
            {
                winner = Team1;
            }
            else if (Team2.Score >= 150)
            {
                winner = Team2;
            }
            else if(Team1.Score >= 150 && Team2.Score >= 150)
            {
                if(Team1.Score > Team2.Score)
                {
                    winner = Team1;
                }
                else if(Team2.Score > Team1.Score)
                {
                    winner = Team2;
                }
                else
                {
                    winner = null; // It's a tie
                }
            }
            else if(Team1.Score - Team2.Score >= 150)
            {
                winner = Team1;
            }
            else if(Team2.Score - Team1.Score >= 150)
            {
                winner = Team2;
            }
        }
    }
}
