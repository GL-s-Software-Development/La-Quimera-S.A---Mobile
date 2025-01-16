using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaQuimera.Models
{
    public class ProductModel
    {
        public int CodeSynAgro { get; set; }
        public string Product { get; set; }
        public string TipoProduct { get; set; }
        public string Deposito { get; set; }
        public int Cantidad { get; set; }
        public string Medida { get; set; }
        public byte[] QrCodeImage { get; set; }
    }
}
