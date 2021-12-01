using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boutique.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "you forgot to write the Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "you forgot to write the Type")]
        public string ProductType { get; set; }
        [Required(ErrorMessage = "you forgot to write the price")]
        public float ProductPrice { get; set; }
        [Required(ErrorMessage = "you forgot to inject the image")]
        public string ProductImg { get; set; }
        [Required(ErrorMessage = "you forgot to write the Quantite")]
        public int QuantiteDispo { get; set; }
        public IList<ProductVente> ProductVentes { get; set; }
        public IList<CommandeProduct> CommandeProducts { get; set; }




    }
}
