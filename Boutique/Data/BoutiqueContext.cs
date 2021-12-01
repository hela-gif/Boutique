using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Boutique.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Boutique.Data
{
    public class BoutiqueContext : IdentityDbContext<Personne>
    {
        public BoutiqueContext (DbContextOptions<BoutiqueContext> options)
            : base(options)
        {
        }

        public DbSet<Boutique.Models.Client> Client { get; set; }

        public DbSet<Boutique.Models.Commande> Commande { get; set; }

        public DbSet<Boutique.Models.CommandeProduct> CommandeProduct { get; set; }

        public DbSet<Boutique.Models.Employe> Employe { get; set; }

        public DbSet<Boutique.Models.Fournisseur> Fournisseur { get; set; }

        public DbSet<Boutique.Models.Livraison> Livraison { get; set; }

        public DbSet<Boutique.Models.Product> Product { get; set; }

        public DbSet<Boutique.Models.ProductVente> ProductVente { get; set; }

        public DbSet<Boutique.Models.Vente> Vente { get; set; }
    }
}
