using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class GetGameFiles
    {
        private static string GetEpicDirectory()
        {
            if (Directory.Exists("C:\\ProgramData\\Epic"))
                return "C:\\ProgramData\\Epic";
            if (Directory.Exists("D:\\ProgramData\\Epic"))
                return "D:\\ProgramData\\Epic";
            return !Directory.Exists("E:\\ProgramData\\Epic") ? "F:\\ProgramData\\Epic" : "E:\\ProgramData\\Epic";
        }

        private static bool DatFileExists => File.Exists(GetGameFiles.GetEpicDirectory() + "\\UnrealEngineLauncher\\LauncherInstalled.dat");

        public static string GetFortniteFiles()
        {
            if (DatFileExists)
            {
                string strInput = File.ReadAllText(GetGameFiles.GetEpicDirectory() + "\\UnrealEngineLauncher\\LauncherInstalled.dat");
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
}
