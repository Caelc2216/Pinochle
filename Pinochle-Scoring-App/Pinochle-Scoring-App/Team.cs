using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinochle_Scoring_App
{
        public partial class Team : ObservableObject
        {
            [ObservableProperty]
            public int score;

           [ObservableProperty]
              public string name;

        [ObservableProperty]
        public int bid;

        [ObservableProperty]
        public int meld;

        [ObservableProperty]
        public int trickPoints;


    }

}
