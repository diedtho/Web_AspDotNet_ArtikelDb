using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web_AspDotNet_ArtikelDb.Models
{
    public class AddArtikel
    {
        [Required]
        public string Bezeichnung { get; set; }

        [Required]
        public string Preis { get; set; }

        [Required]
        public IFormFile Bild { get; set; }

    }
}
