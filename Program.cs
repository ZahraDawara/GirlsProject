using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace rename
{
	internal class Program
	{

		static void Main(string[] args)
		{
            bool fertig = false;

            while (fertig == false)
            {
				Console.Clear();

            Console.Write("Geben Sie den Dateipfad ein um weiterzukommen:");
			string directoryPath = Console.ReadLine();

			Console.WriteLine(" 1. Prefix ändern\n 2. Suffix ändern\n 3. Präfix löschen\n 4. Suffix löschen\n 5. Teilausdrücke wechseln\n 6. Fertig");
            Console.Write("Geben Sie die Nummer von dem gewünschten Vorgang ein:");
            int choosenCase = Int32.Parse(Console.ReadLine());

		

			// Dateinamen lesen
			List<string> fileNamen = GetFileNames(directoryPath);

			

			switch (choosenCase)
			{
				case 1:

					Console.Write("Geben Sie den Prefix ein, welcher geändert werden soll: ");
					string PrefixBefore = Console.ReadLine();
					Console.Write("Geben Sie den Prefix ein, welcher anstatt dem alten verwendet werden soll: ");
					string PrefixAfter = Console.ReadLine();
					// Präfix ändern
					var changedRaeflixNames = ChangePrefixName(fileNamen, PrefixBefore, PrefixAfter);
					RenameFiles(changedRaeflixNames);
						Console.ReadKey();

					break;
				case 2:
					Console.Write("Geben Sie den Suffix ein, welcher geändert werden soll: ");
					string SuffixBefore = Console.ReadLine();
					Console.Write("Geben Sie den Suffix ein, welcher anstatt dem alten verwendet werden soll: ");
					string SuffixAfter = Console.ReadLine();
					// Suffix ändern
					var changedSuffixNames = ChangeSuffixNames(fileNamen, SuffixBefore, SuffixAfter);
					RenameFiles(changedSuffixNames);
                        Console.ReadKey();

                        break;
				case 3:
					Console.Write("Geben Sie den Prefix ein, welcher gelöscht werden soll: ");
					string PrefixDelete = Console.ReadLine();
					// Präfix entfernen
					var deletedPrefixNames = DeletePrefixName(fileNamen, PrefixDelete);
					RenameFiles(deletedPrefixNames);
                        Console.ReadKey();

                        break;
				case 4:
					Console.Write("Geben Sie den Suffix ein, welcher gelöscht werden soll: ");
					string SuffixDelete = Console.ReadLine();
					// Suffix entfernen
					var deletedSuffixNames = DeleteSuffixName(fileNamen, SuffixDelete);
					RenameFiles(deletedSuffixNames);
                        Console.ReadKey();

                        break;
				case 5:
					Console.Write("Geben Sie den Teilausdrück ein, welcher geändert werden soll: ");
					string oldPattern = Console.ReadLine();
					Console.Write("Geben Sie den Teilausdrück ein, welcher anstatt dem alten verwendet werden soll: ");
					string newPattern = Console.ReadLine();

					TeilausdrueckeWechsel( directoryPath, oldPattern, newPattern);
                        Console.ReadKey();

                        break;
						case 6:
						fertig = true;
						break;
                }


            }

			
		}

		// Methode, um alle Dateinamen aus einem Verzeichnis zu lesen
		public static List<string> GetFileNames(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Console.WriteLine("Das angegebene Verzeichnis existiert nicht.");
				return new List<string>();
			}

			return Directory.GetFiles(directoryPath).ToList();
		}

		// Methode zum Umbenennen von Dateien basierend auf einer Zuordnung
		public static void RenameFiles(Dictionary<string, string> fileMappings)
		{
			foreach (var entry in fileMappings)
			{
				string oldFilePath = entry.Key;
				string newFilePath = entry.Value;

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

		// Methode zum Ändern des Präfixes
		public static Dictionary<string, string> ChangePrefixName(List<string> fileNamen, string prefixOld, string prefixNew)
		{
			Dictionary<string, string> myDictionary = new Dictionary<string, string>();

			foreach (var fileName in fileNamen)
			{
				string fileNameOnly = Path.GetFileName(fileName);
				if (fileNameOnly.StartsWith(prefixOld))
				{
					string newFileName = prefixNew + fileNameOnly.Substring(prefixOld.Length);
					string newFilePath = Path.Combine(Path.GetDirectoryName(fileName), newFileName);
					myDictionary.Add(fileName, newFilePath);
				}
			}

			return myDictionary;
		}

		// Methode zum Ändern des Suffixes
		public static Dictionary<string, string> ChangeSuffixNames(List<string> fileNamen, string suffixOld, string suffixNew)
		{
			Dictionary<string, string> myDictionary = new Dictionary<string, string>();

			foreach (var fileName in fileNamen)
			{
				string fileNameOnly = Path.GetFileName(fileName);
				if (fileNameOnly.EndsWith(suffixOld))
				{
					string newFileName = fileNameOnly.Substring(0, fileNameOnly.Length - suffixOld.Length) + suffixNew;
					string newFilePath = Path.Combine(Path.GetDirectoryName(fileName), newFileName);
					myDictionary.Add(fileName, newFilePath);
				}
			}

			return myDictionary;
		}

		// Methode zum Löschen des Präfixes
		public static Dictionary<string, string> DeletePrefixName(List<string> fileNamen, string prefix)
		{
			Dictionary<string, string> myDictionary = new Dictionary<string, string>();

			foreach (var fileName in fileNamen)
			{
				string fileNameOnly = Path.GetFileName(fileName);
				if (fileNameOnly.StartsWith(prefix))
				{
					string newFileName = fileNameOnly.Substring(prefix.Length);
					string newFilePath = Path.Combine(Path.GetDirectoryName(fileName), newFileName);
					myDictionary.Add(fileName, newFilePath);
				}
			}

			return myDictionary;
		}

		// Optional: Methode zum Löschen des Suffixes
		public static Dictionary<string, string> DeleteSuffixName(List<string> fileNamen, string suffix)
		{
			Dictionary<string, string> myDictionary = new Dictionary<string, string>();

			foreach (var fileName in fileNamen)
			{
				string fileNameOnly = Path.GetFileName(fileName);
				if (fileNameOnly.EndsWith(suffix))
				{
					string newFileName = fileNameOnly.Substring(0, fileNameOnly.Length - suffix.Length);
					string newFilePath = Path.Combine(Path.GetDirectoryName(fileName), newFileName);
					myDictionary.Add(fileName, newFilePath);
				}
			}

			return myDictionary;
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

