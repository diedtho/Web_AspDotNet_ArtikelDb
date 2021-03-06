using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_AspDotNet_ArtikelDb.Models
{
    public class Artikel
    {
        [Display(Name="Artikel-Id")]
        public int AId { get; set; }

        [Display(Name = "Bezeichnung")]
        public string Bezeichnung { get; set; }

        [Display(Name = "Preis")]
        public string Preis { get; set; }

        [Display(Name = "Bildname")]
        public string Bildname { get; set; }
    }
}
