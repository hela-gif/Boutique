using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Models
{


    public enum Mode
    {
        Cheque     ,
        Liquide 
    }


    public class Vente
    {
        public int VenteId { get; set; }

        public float Montant { get; set; }


        public Mode  Mode_payment { get; set; }

        public IList<ProductVente> ProductVentes { get; set; }
        //public int QuantiteDesi { get; set; }


        public Client Client { get; set; }

    }
}
