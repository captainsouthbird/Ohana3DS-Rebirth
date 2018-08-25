using System;
using System.IO;
using System.Linq;

namespace OhanaTexture
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine($@"USAGE: 
    {"\t"}OhanaTexture export [model] [directory]
    {"\t\t"}Exports all textures from [model] into [directory]

    {"\t"}OhanaTexture import [model] [directory]
    {"\t\t"}Imports all textures from [directory] into [model]

    ");
            }
            else
            {
                var action = args[0].ToLower();

                if (!File.Exists(args[1]))
                {
                    Console.WriteLine($"ERROR: Model file '{args[1]}' was not found!");
                    return;
                }

                if (!Directory.Exists(args[2]))
                {
                    Console.WriteLine($"ERROR: Texture directory '{args[2]}' was not found!");
                    return;
                }

                if (action == "export")
                {
                    TextureImportExportHelper.Export(args[1], args[2]);
                }
                else if (action == "import")
                {
                    TextureImportExportHelper.Import(args[1], args[2]);
                }
                else
                {
                    Console.WriteLine($"ERROR: Unknown operation {action}");
                }
            }
        }
    }
}
