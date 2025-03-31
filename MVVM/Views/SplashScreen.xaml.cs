namespace MVVM.Views;

public partial class SplashScreen : ContentPage
{
    public SplashScreen()
    {
        InitializeComponent();
        NavigateToAuthSelection();
    }

    private async void NavigateToAuthSelection()
    {
        await Task.Delay(3000); // 3 segundos de splash
        await Shell.Current.GoToAsync("//AuthSelection");
    }
}