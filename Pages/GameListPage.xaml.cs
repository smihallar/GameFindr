using GameFindr.Viewmodels;
using GameFindr.Data.Models;

namespace GameFindr.Pages;

[QueryProperty(nameof(Query), "query")]
public partial class GameListPage : ContentPage
{
	readonly GameListViewModel vm;
	public GameListPage(GameListViewModel vm)
	{
		InitializeComponent();
		this.vm = vm;
		BindingContext = vm;
    }

	public string? Query
	{
		set => vm.Query = value;
	}

    async void ResultsCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as Game;
        if (selected is null)
            return;

        if (sender is CollectionView cv)
            cv.SelectedItem = null;

        // navigate to details page with game id
        var id = selected.GameBrainId?.ToString() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(id))
        {
            await Shell.Current.GoToAsync($"{nameof(GameDetailsPage)}?gameId={System.Uri.EscapeDataString(id)}");
        }
    }
}