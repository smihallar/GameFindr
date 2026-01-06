using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameFindr.Data.Models;
using GameFindr.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    // Query is passed via Shell navigation
    [QueryProperty(nameof(Query), "query")]
    public partial class GameListViewModel : ViewModelBase
    {
        readonly GameService gameService;

        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();

        public GameListViewModel(GameService gameService)
        {
            this.gameService = gameService;
        }

        [ObservableProperty]
        string? query;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string? errorMessage;

        partial void OnQueryChanged(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            _ = LoadInitialAsync();
        }

        public async Task LoadInitialAsync()
        {
            IsLoading = true;
            Games.Clear();
            ErrorMessage = null;

            try
            {
                var results = await gameService.GetGamesBySearchAsync(Query!, 0);
                foreach (var g in results)
                    Games.Add(g);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Shell.Current.Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                });
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
