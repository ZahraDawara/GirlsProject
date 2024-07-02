using System;
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

		static void FuehrendeNullen()
		{
			string[] fileNames = ReadFileNames();
			int zahl = 0;
			bool zahlVorne = false;

			foreach(string name in fileNames)
			{
				string[] nameSplit = name.Split('.');

				string[] firstPart = nameSplit[0].Split('-');

				try
				{
					zahl = Int32.Parse(firstPart[0]);
					zahlVorne = true;
				}
				catch(Exception e)
				{
					zahlVorne = false;
				}

				if(zahlVorne == true)
				{
					if (zahl < 10)
					{
						firstPart[0] = "00" + zahl;
					}
					else if (zahl > 10 && zahl < 100)
					{
						firstPart[0] = "0" + zahl;
					}
				}

			}
		}
    }
}
