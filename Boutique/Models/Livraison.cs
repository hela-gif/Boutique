using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Models
{
   public class Livraison
    {
        public int LivraisonId { get; set; }
        public string adresseLivraison { get; set; }

        public Employe Employe { get; set; }
        
        public Vente Vente { get; set; }

    }
}
