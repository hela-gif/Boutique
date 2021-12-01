using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Models
{
    public class Employe
    {   [Key]
        public int EmployeId { get; set; }
        public  Personne User { get; set; }
        public string PersonneId { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Addresse { get; set; }
        public int Telephone { get; set; }
        public int Age { get; set; }

    }
}
