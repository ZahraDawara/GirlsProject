using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace rename
{
    internal class Program
    {

        static void Main(string[] args)
		{
			
		}

		static string[] ReadFileNames()
		{
			string folderPath = @"C:\Users\fd\Desktop\Projekt_Rename\Biler\";
			string[] fileEntries = Directory.GetFiles(folderPath);
			string[] fileNamen = new string[fileEntries.Length];

			foreach (string fileEntry in fileEntries)
			{
				string filename = Path.GetFileName(fileEntry);
				fileNamen.Append(filename);
			}

			return fileNamen;

		}
 }
}
