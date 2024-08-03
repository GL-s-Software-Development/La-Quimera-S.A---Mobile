using LaQuimera.Views;

namespace LaQuimera
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home());
        }
    }
}
