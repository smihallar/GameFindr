using GameFindr.Viewmodels;

namespace GameFindr
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Title = "GameFindr";
        }

        
    }
}
