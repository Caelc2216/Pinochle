using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Pinochle_Scoring_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartNewGameButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamEntryPage());
        }
    }
}
