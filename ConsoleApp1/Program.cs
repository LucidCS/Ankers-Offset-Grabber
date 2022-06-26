// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;
using ConsoleApp1;
using Newtonsoft.Json;
using System;

namespace ConsoleApp1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static Anker? Anker;
        static void Main(string[] args)
        {
            Redo();
            static void Redo()
            {
                // stuff we just want to put 
                Console.Title = "Ankers Offset Grabber";
                //

                // intalizing the provider so we can use CUE4Parse to export Offsets, and Ucas files
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (Anker.goingagain == true)
                {
                    Console.Clear();
                    Console.WriteLine($"Paks folder: {Anker.Paks}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Intalizing Provider...");
                    Anker = new Anker(Testing.GetGameFiles());
                    Console.WriteLine($"\nPaks folder: {Anker.Paks}");
                }
                //

                // showing the user there paks folder

                // getting the offset
                Console.WriteLine("\nType the asset path of the asset you want to the Offset, and Ucas file of - ↓");
                Console.ForegroundColor = ConsoleColor.Red;
                string assetPath = Console.ReadLine();
                Anker.SaveAsset(assetPath);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Got Offset and Ucas!");

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("\n\nInfo:");
                if (assetPath.Contains("FortniteGame"))
                    assetPath = assetPath.Replace("FortniteGame", "/Game");
                if (assetPath.Contains(".uasset"))
                    assetPath = assetPath.Replace(".uasset", "");
                if (assetPath.Contains(@"\"))
                    assetPath = assetPath.Replace(@"\", "/");

                Console.WriteLine($"Uasset: {assetPath}");
                Console.WriteLine($"Offset: {Anker.Offset}");
                Console.WriteLine($"Ucas: {Anker.Ucas.Replace(Anker.Paks + @"\", "")}");
                Console.WriteLine($"Compressed: {Anker.Compressed}");
                Console.WriteLine("\nWould you like to get another assets Ucas/Offset? Y = Yes, N = No");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    Anker.goingagain = true;
                    Redo();
                }
                else
                    Environment.Exit(0);
            }

        }
    }
}
public static class Testing
{


    private static string GetEpicDirectory()
    {
        if (Directory.Exists("C:\\ProgramData\\Epic"))
            return "C:\\ProgramData\\Epic";
        if (Directory.Exists("D:\\ProgramData\\Epic"))
            return "D:\\ProgramData\\Epic";
        return !Directory.Exists("E:\\ProgramData\\Epic") ? "F:\\ProgramData\\Epic" : "E:\\ProgramData\\Epic";
    }

    private static bool DatFileExists => File.Exists(Testing.GetEpicDirectory() + "\\UnrealEngineLauncher\\LauncherInstalled.dat");

    public static string GetGameFiles()
    {
        if (DatFileExists)
        {
            string strInput = File.ReadAllText(Testing.GetEpicDirectory() + "\\UnrealEngineLauncher\\LauncherInstalled.dat");
            if (Findpaks.IsValidJson(strInput))
            {
                JToken jtoken1 = JsonConvert.DeserializeObject<JToken>(strInput);
                if (jtoken1 != null)
                {
                    JArray jarray = jtoken1[(object)"InstallationList"].Value<JArray>();
                    if (jarray != null)
                    {
                        foreach (JToken jtoken2 in jarray)
                        {
                            if (string.Equals(jtoken2[(object)"AppName"].Value<string>(), "Fortnite"))
                                return jtoken2[(object)"InstallLocation"].Value<string>() + "\\FortniteGame\\Content\\Paks";
                        }
                    }
                }
            }
        }
        return string.Empty;
    }
}
