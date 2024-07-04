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
			//("C:\\Users\\03717\\Pictures\\Test");

			Console.ReadKey();
		}

		static void AnalyseMuster(string endung, string präfix)
		{
			
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

		public static void ChangeNumbers(Dictionary<string, string> fileMappings)
		{

		}

    }
}

