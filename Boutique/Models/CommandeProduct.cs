using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boutique.Models
{
    public class CommandeProduct
    {
        public int CommandeProductId { get; set; }
        public int ProductId { get; set; }
        public int CommandeId { get; set; }
        public Product Product { get; set; }
        public Commande Commande { get; set; }
        public int Quantite { get; set; }
    }
}
