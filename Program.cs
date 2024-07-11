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

    }
}
