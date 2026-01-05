using CommunityToolkit.Mvvm.ComponentModel;
using GameFindr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameFindr.Viewmodels
{
    public partial class GameDetailsViewModel : ViewModelBase
    {
        [ObservableProperty]
        Game game;
    }
}
