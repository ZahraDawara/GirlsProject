using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace rename
{
	internal class Program
	{

		static void Main(string[] args)
		{
			// Beispielverzeichnis angeben (muss angepasst werden)
			string directoryPath = @"C:\Verzeichnis";

			// Dateinamen lesen
			List<string> fileNamen = GetFileNames(directoryPath);

			// Präfix ändern
			var changedRaeflixNames = ChangePrefixName(fileNamen, "img", "Image");
			RenameFiles(changedRaeflixNames);

			// Suffix ändern
			var changedSuffixNames = ChangeSuffixNames(fileNamen, "jpg", "txt");
			RenameFiles(changedSuffixNames);

			// Präfix entfernen
			var deletedPrefixNames = DeletePrefixName(fileNamen, "img");
			RenameFiles(deletedPrefixNames);
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
	}
}
