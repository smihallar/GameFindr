using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameFindr.Data.Models;
using GameFindr.Pages;
using GameFindr.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    public partial class MainViewModel : ViewModelBase
    {
        readonly GameService gameService;

        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();


        public MainViewModel(GameService gameService)
        {
            this.gameService = gameService;
        }

        [ObservableProperty]
        string? searchQuery;

        [ObservableProperty]
        string? errorMessage;

        [RelayCommand]
        async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                ErrorMessage = "Please enter a valid search, for example a game name or a genre";
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(GameListPage)}?query={System.Uri.EscapeDataString(SearchQuery)}");

        }
    }
}
