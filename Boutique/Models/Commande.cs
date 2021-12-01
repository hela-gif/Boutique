using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public string dateCommande { get; set; }
        public string dateLivraison { get; set; }
        //public int Quantite { get; set; }
        public Employe Employe { get; set; }
        public IList<CommandeProduct> CommandeProducts { get; set; }
        public Fournisseur Fournisseur { get; set; }

    }
}
