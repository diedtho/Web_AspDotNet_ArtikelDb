using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_AspDotNet_ArtikelDb.Models
{
    public class UpdateArtikel
    {
        public int AId { get; set; }
        public string Bezeichnung { get; set; }
        public string Preis { get; set; }
        public string Bildname { get; set; }
        public IFormFile Bild { get; set; }
    }
}
