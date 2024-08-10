using ZXing.Net.Maui;

namespace LaQuimera.Views;

public partial class ScanPage : ContentPage
{
    private bool isScanning = false; // Flag para controlar el escaneo

    public ScanPage()
    {
        InitializeComponent();
    }

    private async void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (isScanning)
            return; // Si ya est� escaneando, salir

        isScanning = true; // Marcar como escaneando

        var barcode = e.Results.FirstOrDefault()?.Value;
        if (barcode != null)
        {
            // Actualizar la UI en el hilo principal
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // Navegar a la p�gina de resultados y pasar el c�digo QR
                await Navigation.PushAsync(new ResultPage(barcode));

                // Deshabilitar temporalmente el esc�ner o agregar una pausa si es necesario
                await Task.Delay(1000); // Esto puede ser opcional, dependiendo de tus necesidades
            });
        }

        isScanning = false; // Restablecer el flag
    }
}