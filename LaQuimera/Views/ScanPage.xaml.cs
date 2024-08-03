using ZXing.Net.Maui;

namespace LaQuimera.Views;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();
    }

    private async void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var barcode = e.Results.FirstOrDefault()?.Value;
        if (barcode != null)
        {
            // Actualizar la UI en el hilo principal
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // Navegar a la página de resultados y pasar el código QR
                await Navigation.PushAsync(new ResultPage(barcode));
            });
        }
    }
}