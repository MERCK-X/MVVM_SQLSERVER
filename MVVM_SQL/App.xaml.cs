using MVVM_SQL.View;

namespace MVVM_SQL
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            MainPage = new NavigationPage(new CRUDPage());
        }
    }
}
