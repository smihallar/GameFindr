using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameFindr.Data.Models;
using GameFindr.Services;
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

        [RelayCommand]
        async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
                return;

            IsLoading = true;
            offset = 0;
            HasMore = true;
            Games.Clear();

            var results = await gameService.GetGamesBySearchAsync(SearchQuery, offset);
            foreach (var g in results)
                Games.Add(g);

            offset += results.Count;
            if (results.Count < PageSize)
                HasMore = false;

            IsLoading = false;
        }

        [RelayCommand]
        async Task LoadMoreAsync()
        {
            if (IsLoading || !HasMore || string.IsNullOrWhiteSpace(SearchQuery))
                return;

            IsLoading = true;
            var results = await gameService.GetGamesBySearchAsync(SearchQuery, offset);
            foreach (var g in results)
                Games.Add(g);

            offset += results.Count;
            if (results.Count < PageSize)
                HasMore = false;
            IsLoading = false;
        }
    }
}
