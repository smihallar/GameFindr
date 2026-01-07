using CommunityToolkit.Mvvm.ComponentModel;
using GameFindr.Data.Models;
using GameFindr.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    [QueryProperty(nameof(GameId), "gameId")]
    public partial class SimilarGamesViewModel : ViewModelBase
    {
        readonly GameService gameService;

        public ObservableCollection<Game> SimilarGames { get; } = new ObservableCollection<Game>();

        [ObservableProperty]
        string? gameId;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string? errorMessage;

        public SimilarGamesViewModel(GameService gameService)
        {
            this.gameService = gameService;
        }

        partial void OnGameIdChanged(string? value)
        {
            if (int.TryParse(value, out var id))
                _ = LoadAsync(id);
        }

        public async Task LoadAsync(int id)
        {
            IsLoading = true;
            ErrorMessage = null;
            SimilarGames.Clear();

            try
            {
                var results = await gameService.GetSimilarGamesByIdAsync(id);
                if (results?.Count > 0)
                {
                    foreach (var g in results)
                        SimilarGames.Add(g);
                }
                else
                {
                    ErrorMessage = "No similar games found.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
