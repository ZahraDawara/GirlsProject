using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rename
{
    internal class Program
    {

        static void Main(string[] args)
        { // Datei zugreifen
            var folderPath = @"C:\Users\fd\Desktop\Projekt_Rename\Biler\";
            var fileEntries = Directory.GetFiles(folderPath);
            string[] fileNamen = new string[fileEntries.Length];

            foreach (var fileEntry in fileEntries)
                {
                    var filename = Path.GetFileName(fileEntry);
                    Console.WriteLine(filename);
                }
            
            Console.ReadKey();
           

        }
    }
}
