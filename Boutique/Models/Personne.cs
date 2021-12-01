
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Models

{
    public class Personne :IdentityUser
    {
        public virtual Employe Employe { get; set; }
        public virtual Client Client { get; set; }

    }
}
