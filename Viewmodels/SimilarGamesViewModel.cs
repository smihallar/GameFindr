using CommunityToolkit.Mvvm.ComponentModel;
using GameFindr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    [QueryProperty("SimilarGames", "SimilarGames")]
    public partial class SimilarGamesViewModel : ViewModelBase
    {
        [ObservableProperty]
        List<Game> similarGames;
    }
}
