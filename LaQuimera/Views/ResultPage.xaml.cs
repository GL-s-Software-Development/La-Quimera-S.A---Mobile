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
                    FormattedText = new FormattedString
                    {
                        Spans =
                        {
                            new Span { Text = "Producto: ", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gray },
                            new Span { Text = product.Product ?? "N/A", FontAttributes = FontAttributes.None, TextColor = Colors.Black }
                        }
                    },
                    FontSize = 20
                },
                new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans =
                        {
                            new Span { Text = "Tipo: ", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gray },
                            new Span { Text = product.TipoProduct ?? "N/A", FontAttributes = FontAttributes.None, TextColor = Colors.Black }
                        }
                    },
                    FontSize = 18
                },
                new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans =
                        {
                            new Span { Text = "Deposito: ", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gray },
                            new Span { Text = product.Deposito ?? "N/A", FontAttributes = FontAttributes.None, TextColor = Colors.Black }
                        }
                    },
                    FontSize = 18
                },
                new Label
                {
                    FormattedText = new FormattedString
                    {
                        Spans =
                        {
                            new Span { Text = "Cantidad: ", FontAttributes = FontAttributes.Bold, TextColor = Colors.Gray },
                            new Span { Text= $"{product.Cantidad} ", FontAttributes = FontAttributes.None, TextColor = Colors.Black},
                            new Span { Text = $"{product.Medida}", FontAttributes = FontAttributes.Bold, TextColor = Colors.Black }
                        }
                    },
                    FontSize = 18
                },
                product.QrCodeImage != null
                    ? new Image
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(product.QrCodeImage)),
                        HeightRequest = 100,  // Ajustar el tamaño de la imagen si es necesario
                        WidthRequest = 100
                    }
                    : new Label { Text = "QR Code not available", TextColor = Colors.Gray, FontSize = 18 }
            }
                }
            };

            return productCard;
        }
    }
}