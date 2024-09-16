using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace rename
{
	internal class Program
	{

		static void Main(string[] args)
		{
			string[] fileNamen = ReadFileNames();
			changeRaeflixName(fileNamen,"img","Image");
			changeSuffixnamen(fileNamen,"npg","txt");
			deletePraefixName(fileNamen,"img");
			
		}

		public static List<string> GetFileNames(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Console.WriteLine("Das angegebene Verzeichnis existiert nicht.");
				return new List<string>();
			}

			return Directory.GetFiles(directoryPath).ToList();
		}

		public static Dictionary<string, string> AddLeadingZeros(List<string> files)
		{
			var fileMappings = new Dictionary<string, string>();
			Regex regex = new Regex(@"^(\d+)");
			int maxDigits = files
				.Select(file => regex.Match(Path.GetFileName(file)))
				.Where(match => match.Success)
				.Select(match => match.Groups[1].Value.Length)
				.DefaultIfEmpty(0)
				.Max();

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);
				Match match = regex.Match(fileName);
				if (match.Success)
				{
					string numberStr = match.Groups[1].Value;
					int number = int.Parse(numberStr);
					string newNumberStr = number.ToString().PadLeft(maxDigits, '0');
					string newFileName = regex.Replace(fileName, newNumberStr);
					fileMappings[file] = Path.Combine(Path.GetDirectoryName(file), newFileName);
				}
			}

			return fileMappings;
		}

		public static void RenameFiles(Dictionary<string, string> fileMappings)
		{
			foreach (var entry in fileMappings)
			{
				string oldFilePath = entry.Key;
				string newFilePath = entry.Value;

        for (int i = 0; i < fileEntries.Length; i++)
			{

				string filename = Path.GetFileName(fileEntries[i]);
				fileNamen[i] = filename;
			}
			return fileNamen;
		}
      
		static Dictionary<string, string> changeRaeflixName(string[] fileNamen, string praefixAlt, string praefixNeu)
		{
			string[] changeRaeflixName = new string[fileNamen.Length];

			//Array.Copy(fileNamen, fileNameChange, fileNamen.Length);
			Dictionary<string, string> mydictionary = new Dictionary<string, string>();

			for (int i = 0; i < fileNamen.Length; i++)
			{
				if (fileNamen[i].StartsWith(praefixAlt))
				{
					int praefixAltLen =praefixAlt.Length;
					changeRaeflixName[i] = praefixNeu + fileNamen[i].Substring(praefixAltLen);
					mydictionary.Add(fileNamen[i], changeRaeflixName[i]);
				}
			}
			return mydictionary;
		}
		static Dictionary<string,string> changeSuffixnamen(string[] fileNamen,string suffixAlt, string suffixNeu)
		{
            string[] changeSuffixName = new string[fileNamen.Length];
            Dictionary<string, string> mydictionary = new Dictionary<string, string>();
			
			for(int i = 0;i < fileNamen.Length; i++)
			{
				if (fileNamen[i].EndsWith(suffixAlt))
				{ int suffixAltLen = suffixAlt.Length;
					int fileNameLen = fileNamen[i].Count();
					changeSuffixName[i] = fileNamen[i].Substring(0, fileNameLen-suffixAltLen)+suffixNeu;
					mydictionary.Add(fileNamen[i], changeSuffixName[i]);
				}
			}
            return mydictionary;
        }
		static Dictionary<string,string> deletePraefixName(string[] fileNamen, string praefix)
		{
            string[] deletePraefixName = new string[fileNamen.Length];
            Dictionary<string, string> mydictionary = new Dictionary<string, string>();
            for (int i = 0; i < fileNamen.Length; i++)
			{
				if (fileNamen[i].StartsWith(praefix))
				{
					string newFilename = fileNamen[i].Substring(4);
					mydictionary.Add(fileNamen[i], deletePraefixName[i]);
				}
			}
			return mydictionary;


				try
				{
					File.Move(oldFilePath, newFilePath);
					Console.WriteLine($"Datei umbenannt: {Path.GetFileName(oldFilePath)} -> {Path.GetFileName(newFilePath)}");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Fehler beim Umbenennen der Datei {Path.GetFileName(oldFilePath)}: {ex.Message}");
				}
			}
		}
        static Dictionary<string, string> deleteSuffixName(string[] fileNamen, string suffix)
        {
            string[] deleteSuffixName = new string[fileNamen.Length];
            Dictionary<string, string> mydictionary = new Dictionary<string, string>();
            for (int i = 0; i < fileNamen.Length; i++)
            {
                if (fileNamen[i].EndsWith(suffix))
                {
					int suffixlen = suffix.Length;
                    int fileNameLen = fileNamen[i].Count();
                    deleteSuffixName[i] = fileNamen[i].Substring(0, fileNameLen - suffixlen) ;
                    mydictionary.Add(fileNamen[i], deleteSuffixName[i]);
                }
            }
            return mydictionary;

        }

		// Niginakhon Shukurova - Teilausdruecke umbennnen

		public static void TeilausdrueckeWechsel(string directoryPath, string oldPattern, string newPattern)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] allFiles = Directory.GetFiles(directoryPath);

                    foreach (string file in allFiles)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExtension = Path.GetExtension(file);
                        string newFileName = fileName;

                        if (fileName.Contains(oldPattern))
                        {
                            newFileName = fileName.Replace(oldPattern, newPattern);
                            string newFilePath = Path.Combine(directoryPath, newFileName + fileExtension);
                            File.Move(file, newFilePath);

                            Console.WriteLine($"Renamed: {file} -> {newFilePath}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Directory does not exist!");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error renaming files: " + ex.Message);
            }
        }
    }
}

