namespace LaQuimera.Views;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
    }
    private async void OnScanButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanPage());
    }
}