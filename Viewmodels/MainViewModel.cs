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

        const int PageSize = 10;
        int offset = 0;

        public MainViewModel(GameService gameService)
        {
            this.gameService = gameService;
        }

        [ObservableProperty]
        string? searchQuery;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        bool hasMore = true;

        [ObservableProperty]
        string? errorMessage;

        [RelayCommand]
        async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
                return;

            await Shell.Current.GoToAsync($"{nameof(GameListPage)}?query={System.Uri.EscapeDataString(SearchQuery)}");

        }

        [RelayCommand]
        async Task LoadMoreAsync()
        {
            if (IsLoading || !HasMore || string.IsNullOrWhiteSpace(SearchQuery))
                return;

            IsLoading = true;
            ErrorMessage = null;

            try
            {
                var results = await gameService.GetGamesBySearchAsync(SearchQuery, offset);
                foreach (var g in results)
                    Games.Add(g);

                offset += results.Count;
                if (results.Count < PageSize)
                    HasMore = false;
            }
            catch (HttpRequestException httpEx)
            {
                ErrorMessage = httpEx.Message;
                await Shell.Current.DisplayAlert("Network error", httpEx.Message, "OK");
            }
            catch (System.Exception ex)
            {
                ErrorMessage = ex.Message;
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
