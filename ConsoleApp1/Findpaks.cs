using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Findpaks
    {
        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) && (!strInput.StartsWith("[") || !strInput.EndsWith("]")))
                return false;
            try
            {
                JToken.Parse(strInput);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
