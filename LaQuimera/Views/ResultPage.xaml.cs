using LaQuimera.Services;
using LaQuimera.Models;
using Microsoft.Maui.Controls;

namespace LaQuimera.Views
{
    public partial class ResultPage : ContentPage
    {
        private ProductService _productService = new ProductService();

        public ResultPage(string qrCode)
        {
            InitializeComponent();
            LoadProductData(qrCode);
        }

        private async void LoadProductData(string qrCode)
        {
            // Consulta los datos del producto
            var products = await _productService.GetByProductAsync(qrCode);

            // Asegúrate de que la actualización de la UI se realice en el hilo principal
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                //resultsStackLayout.Children.Clear();

                if (products != null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        var productCard = CreateProductCard(product);
                        resultsStackLayout.Children.Add(productCard);
                    }
                }
                else
                {
                    var noResultLabel = new Label
                    {
                        Text = "Producto no encontrado",
                        FontSize = 24,
                        HorizontalOptions = LayoutOptions.Center
                    };
                    resultsStackLayout.Children.Add(noResultLabel);
                }
            });
        }

        private Frame CreateProductCard(ProductModel product)
        {
            // Debug: Verifica los valores del producto
            System.Diagnostics.Debug.WriteLine($"Product: {product.Product}, Tipo: {product.TipoProduct}, Deposito: {product.Deposito}, Cantidad: {product.Cantidad}");

            var productCard = new Frame
            {
                BorderColor = Colors.Gray,
                CornerRadius = 10,
                Margin = new Thickness(0, 10),
                Padding = 10,
                BackgroundColor = Colors.White,
                HasShadow = true,
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = $"Producto: {product.Product ?? "N/A"}",
                            FontSize = 20,
                            FontAttributes = FontAttributes.Bold
                        },
                        new Label
                        {
                            Text = $"Tipo: {product.TipoProduct ?? "N/A"}",
                            FontSize = 18
                        },
                        new Label
                        {
                            Text = $"Deposito: {product.Deposito ?? "N/A"}",
                            FontSize = 18
                        },
                        new Label
                        {
                            Text = $"Cantidad: {product.Cantidad}",
                            FontSize = 18
                        },
                        // Si tienes una imagen QR, configúralo aquí
                        product.QrCodeImage != null
                            ? new Image
                            {
                                Source = ImageSource.FromStream(() => new MemoryStream(product.QrCodeImage)),
                                HeightRequest = 100,  // Ajusta el tamaño de la imagen si es necesario
                                WidthRequest = 100
                            }
                            : new Label { Text = "QR Code not available" }
                    }
                }
            };

            return productCard;
        }
    }
}