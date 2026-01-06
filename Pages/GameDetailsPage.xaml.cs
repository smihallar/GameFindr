using GameFindr.Viewmodels;

namespace GameFindr.Pages;
[QueryProperty(nameof(GameId), "gameId")]
public partial class GameDetailsPage : ContentPage
{
    readonly GameDetailsViewModel vm;

    public GameDetailsPage(GameDetailsViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    public string? GameId
    {
        set
        {
            if (int.TryParse(value, out var id))
            {
                _ = vm.LoadAsync(id);
            }
        }
    }
}