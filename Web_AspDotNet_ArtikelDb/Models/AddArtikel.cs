﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web_AspDotNet_ArtikelDb.Models
{
    public class AddArtikel
    {
        public string Bezeichnung { get; set; }
        public string Preis { get; set; }
        public IFormFile Bild { get; set; }
    }
}
