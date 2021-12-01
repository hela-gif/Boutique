using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boutique.Models
{
    public class ProductVente
    {
        public int ProductVenteId { get; set; }
        public int ProductId { get; set; }
        public int VenteId { get; set; }
        public Product Product { get; set; }
        public Vente Vente { get; set; }
        public int QuantiteDesi { get; set; }
    }
}
