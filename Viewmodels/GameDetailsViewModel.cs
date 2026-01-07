using CommunityToolkit.Mvvm.ComponentModel;
using GameFindr.Data.Models;
using GameFindr.Services;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    public partial class GameDetailsViewModel : ViewModelBase
    {
        readonly GameService gameService;

        public GameDetailsViewModel(GameService gameService)
        {
            this.gameService = gameService;
        }

        [ObservableProperty]
        Game? game;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string? errorMessage;

        public async Task LoadAsync(int id)
        {
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                var details = await gameService.GetGameDetailsByIdAsync(id);
                if(details == null)
                {
                    ErrorMessage = "Game details not found.";
                    return;
                }
                Game = details;
            }
            catch (System.Exception ex)
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
