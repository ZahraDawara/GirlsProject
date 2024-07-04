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
			 changeRaeflixName(fileNamen);
			
		}

		static string[] ReadFileNames()
		{
			string folderPath = @"C:\Users\fd\Desktop\Projekt_Rename\Biler\";
			string[] fileEntries = Directory.GetFiles(folderPath);
			string[] fileNamen = new string[fileEntries.Length];

			for (int i = 0; i < fileEntries.Length; i++)
			{

				string filename = Path.GetFileName(fileEntries[i]);
				fileNamen[i] = filename;
			}

			return fileNamen;

		}
		static Dictionary<string, string> changeRaeflixName(string[] fileNamen)
		{
			string[] changeRaeflixName = new string[fileNamen.Length];

			//Array.Copy(fileNamen, fileNameChange, fileNamen.Length);
			Dictionary<string, string> mydictionary = new Dictionary<string, string>();

			for (int i = 0; i < fileNamen.Length; i++)
			{
				if (fileNamen[i].StartsWith("img-"))
				{

					changeRaeflixName[i] = "Image-" + fileNamen[i].Substring(4);
					mydictionary.Add(fileNamen[i], changeRaeflixName[i]);
				}
			}
			return mydictionary;
		}
		static Dictionary<string,string> changeSuffixnamen(string[] fileNamen, string suffix)
		{
            string[] changeSuffixName = new string[fileNamen.Length];
            Dictionary<string, string> mydictionary = new Dictionary<string, string>();
			
			for(int i = 0;i < fileNamen.Length; i++)
			{
				if (fileNamen[i].EndsWith("jpg"))
				{
					int fileNameLen = fileNamen[i].Count();
					changeSuffixName[i] = fileNamen[i].Substring(0, fileNameLen-3)+suffix;
					mydictionary.Add(fileNamen[i], changeSuffixName[i]);
				}
			}
            return mydictionary;
        }
		static Dictionary<string,string> deletePraefixName(string[] fileNamen)
		{
            string[] deletePraefixName = new string[fileNamen.Length];
            Dictionary<string, string> mydictionary = new Dictionary<string, string>();
            for (int i = 0; i < fileNamen.Length; i++)
			{
				if (fileNamen[i].StartsWith("img-"))
				{
					string newFilename = fileNamen[i].Substring(4);
					mydictionary.Add(fileNamen[i], deletePraefixName[i]);
				}
			}
			return mydictionary;

		}

	}
}
