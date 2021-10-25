using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web_AspDotNet_ArtikelDb.Models;

namespace Web_AspDotNet_ArtikelDb
{
    public class HomeController : Controller
    {
        private readonly SqliteConnection conn;

        // Hierin befinden sich alle Informationen zur Umgebung des Webprojekts
        // "readonly" bewirkt, dass es keine Änderungen geben kann (außer durch den Konstruktor beim Erzeugen der neuen Instanz am Anfang)
        // "private" müsste nicht extra gesetzt werden, weil alle Methoden per Standard auf "private" gesetzt sind
        private readonly IWebHostEnvironment InfoUeberWebserver;


        // Konstruktor (für mehrere Zugriffe)
        public HomeController(IWebHostEnvironment _environment)
        {
            // Environment-Informationen zuweisen
            InfoUeberWebserver = _environment;

            // 1. Connection-String
            string connStr = "Data Source =./ArtikelDb.db;";

            // 2. SQL-Connection
            conn = new SqliteConnection(connStr);
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Artikelliste            
            List<Artikel> listeArtikel = new();

            // 3. SQL-Command
            SqliteCommand cmdSql = new SqliteCommand("Select * From Artikel;", conn);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen
            var dr = cmdSql.ExecuteReader();
            while (dr.Read())
            {
                Artikel artikel = new Artikel
                {
                    AId = (int)(long)dr[0],  // "int" = 32bit, "long" = 64bit
                    Bezeichnung = dr[1].ToString(),
                    Preis = ((double)dr[2]).ToString(),
                    Bildname = dr[3].ToString()
                };
                listeArtikel.Add(artikel);
            }

            // 6. Verbindung schließen
            conn.Close();

            // Nutzung des Models
            ListeArtikel listArt = new ListeArtikel { ArtikelListe = listeArtikel };

            return View(listArt);
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(AddArtikel neuerArtikel)
        {
            // 1. Bildname GUId erzeugen und uploaden in Ordner "Bild"
            Bild bild = new();
            bild.file = neuerArtikel.Bild;
            string bildName = SpeichernBild(bild);

            // 2.


            // 3. SQL-Command (insert-Statement)            
            double preis = Double.Parse(neuerArtikel.Preis);
            SqliteCommand cmdSqlInsert = new SqliteCommand($"INSERT INTO Artikel ('Bezeichnung','EPreis','Bild')" +
                $" VALUES(@Bezeichnung,@Preis,@Bildname);", conn);
            cmdSqlInsert.Parameters.AddWithValue("@Bezeichnung", neuerArtikel.Bezeichnung);
            cmdSqlInsert.Parameters.AddWithValue("@Preis", neuerArtikel.Preis);
            cmdSqlInsert.Parameters.AddWithValue("@Bildname", bildName);

            // 4. Verbindung öffnen
            conn.Open();

            // 5. Ergebnis des Selects lesen
            int ok = cmdSqlInsert.ExecuteNonQuery();
            if (ok != 1) { }

            // 6. Verbindung schließen
            conn.Close();

            return RedirectToAction("Index");
        }

        // Bildspeichermethode
        private string SpeichernBild(Bild neuBild)
        {
            string guid = Guid.NewGuid().ToString();
            string wwwPath = this.InfoUeberWebserver.WebRootPath;
            string contentPath = this.InfoUeberWebserver.ContentRootPath;

            // Falls der Ordner "Bilder" noch nicht vorhanden ist, wird er erzeugt
            string path = Path.Combine(this.InfoUeberWebserver.WebRootPath, "Bilder");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Um Files wieder zu löschen
            // System.IO.File.Delete("/wwwroot...");

            // Speichern / Uploaden
            string neuFName = guid + neuBild.file.FileName.Substring(neuBild.file.FileName.LastIndexOf("."));
            using (FileStream Fstream = new FileStream(wwwPath + "/Bilder/" + neuFName, FileMode.Create))
            {
                neuBild.file.CopyTo(Fstream);
                return neuFName;
            }
        }
    }

}
