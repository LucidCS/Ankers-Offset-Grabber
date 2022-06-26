using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Anker
    {
        public static string Paks { get; set; }
        public DefaultFileProvider provider;
        public FGuid FGuid = new FGuid();
        public FAesKey LatestAES = new FAesKey("0x0000000000000000000000000000000000000000000000000000000000000000");
        public static long Offset { get; set; }
        public static string Ucas { get; set; }
        public static bool Compressed { get; set; }


        public Anker(string paks)
        {
            Paks = paks;
            provider = new DefaultFileProvider(paks, SearchOption.TopDirectoryOnly, false, new VersionContainer(EGame.GAME_UE5_LATEST));
            provider.Initialize();
            provider.SubmitKey(FGuid, LatestAES);
        }

        public byte[] SaveAsset(string assetPath)
        {
            byte[] ExportedAsset = provider.SaveAsset(assetPath);
            if (ExportedAsset.Length >= 1024)
                Compressed = true;
            else
                Compressed = false;

            return ExportedAsset;
        }
    }
}
