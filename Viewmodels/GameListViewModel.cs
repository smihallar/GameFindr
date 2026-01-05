using CommunityToolkit.Mvvm.ComponentModel;
using GameFindr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    [QueryProperty("Games", "Games")]
    public partial class GameListViewModel : ViewModelBase
    {
        // For the list of all games
        [ObservableProperty]
        List<Game> games;
    }
}
