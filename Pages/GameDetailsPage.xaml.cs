using System;
using System.Collections;
using System.Linq;
using GameFindr.Viewmodels;
using Microsoft.Maui.Controls;

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

    async void OnShowSimilarClicked(object? sender, EventArgs e)
    {
        if (vm.Game?.GameBrainId == null)
            return;

        var id = vm.Game.GameBrainId.Value.ToString();
        await Shell.Current.GoToAsync($"{nameof(SimilarGamesPage)}?gameId={System.Uri.EscapeDataString(id)}");
    }

    void OnPrevScreenshotClicked(object sender, EventArgs e)
    {
        if (ScreenshotsCarousel?.ItemsSource is IEnumerable items)
        {
            var list = (items as System.Collections.IList) ?? items.Cast<object>().ToList();
            int count = list.Count;
            if (count == 0) return;

            int index = ScreenshotsCarousel.Position;
            int newIndex = (index - 1 + count) % count;
            ScreenshotsCarousel.ScrollTo(newIndex, position: ScrollToPosition.Center, animate: true);
        }
    }

    void OnNextScreenshotClicked(object sender, EventArgs e)
    {
        if (ScreenshotsCarousel?.ItemsSource is IEnumerable items)
        {
            var list = (items as System.Collections.IList) ?? items.Cast<object>().ToList();
            int count = list.Count;
            if (count == 0) return;

            int index = ScreenshotsCarousel.Position;
            int newIndex = (index + 1) % count;
            ScreenshotsCarousel.ScrollTo(newIndex, position: ScrollToPosition.Center, animate: true);
        }
    }
}