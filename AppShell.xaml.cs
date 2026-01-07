using GameFindr.Pages;

namespace GameFindr
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GameListPage), typeof(GameListPage));
            Routing.RegisterRoute(nameof(GameDetailsPage), typeof(GameDetailsPage));
            Routing.RegisterRoute(nameof(SimilarGamesPage), typeof(SimilarGamesPage));
        }
    }
}
