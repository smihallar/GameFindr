using GameFindr.Viewmodels;
using GameFindr.Data.Models;

namespace GameFindr.Pages;

[QueryProperty(nameof(GameId), "gameId")]
public partial class SimilarGamesPage : ContentPage
{
    readonly SimilarGamesViewModel vm;

    public SimilarGamesPage(SimilarGamesViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    public string? GameId
    {
        set => vm.GameId = value;
    }


    async void ResultsCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as Game;
        if (selected is null)
            return;

        if (sender is CollectionView cv)
            cv.SelectedItem = null;

        var id = selected.GameBrainId?.ToString() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(id))
        {
            await Shell.Current.GoToAsync($"{nameof(GameDetailsPage)}?gameId={System.Uri.EscapeDataString(id)}");
        }
    }
}