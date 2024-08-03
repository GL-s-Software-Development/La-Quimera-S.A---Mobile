using LaQuimera.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaQuimera.Services
{
    public class ProductService
    {
        private string connectionString = "Server=192.168.0.250; Port=3306; Database=laquimera; User=lquser; Pwd=Arcadia1@;";

        public async Task<List<ProductModel>> GetByProductAsync(string searchProduct)
        {
            List<ProductModel> products = new List<ProductModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM PRODUCTOS_INVENTORY WHERE CODSYN_PRODUCTO LIKE @searchString OR PRODUCTO LIKE @searchString";
                    command.Parameters.Add("@searchString", MySqlDbType.VarChar).Value = "%" + searchProduct + "%";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ProductModel product = new ProductModel()
                            {
                                CodeSynAgro = Convert.ToInt32(reader["CODSYN_PRODUCTO"]),
                                Product = reader["PRODUCTO"].ToString(),
                                TipoProduct = reader["TIPO_PRODUC"].ToString(),
                                Deposito = reader["DEPOSITO"].ToString(),
                                Cantidad = Convert.ToInt32(reader["CANTIDAD"]),
                                QrCodeImage = reader["CODIGO_QR"] != DBNull.Value ? (byte[])reader["CODIGO_QR"] : null
                                // ... Otros campos
                            };
                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }
    }
}
