using System;
using System.IO;
using System.Linq;

namespace OhanaTexture
{
    public static class TextureImportExportHelper
    {
        public static void Export(string modelFilename, string textureDir)
        {
            Common.MyOhana.Load_Textures(modelFilename);

            // Dump all textures
            var textures = Common.MyOhana.Model_Texture;
            foreach (var texture in textures)
            {
                texture.Image.Save(Path.Combine(textureDir, texture.Name + ".png"));
            }
        }

        public static void Import(string modelFilename, string textureDir)
        {
            Common.MyOhana.Load_Textures(modelFilename);

            // Get all textures
            var textureFiles = Directory
                .GetFiles(textureDir, "*.png")
                .Select(f => new
                {
                    FileName = f,
                    TextureName = Path.GetFileNameWithoutExtension(f)
                });

            var textures = Common.MyOhana.Model_Texture;

            // Textures need to line up to actual textures for replacement,
            // we'll warn if they don't.                   
            foreach (var warnTextureFile in textureFiles
                .Where(tf => !textures.Where(t => t.Name == tf.TextureName).Any()))
            {
                Console.WriteLine($"WARNING: {warnTextureFile.FileName} has no matching model texture");
            }

            for (var i = 0; i < textures.Count; i++)
            {
                // Name of texture in model
                var textureName = textures[i].Name;

                // See if we have a file that matches
                var replacementTexture = textureFiles
                    .Where(f => f.TextureName == textureName)
                    .SingleOrDefault();

                if (replacementTexture != null)
                {
                    Common.MyOhana.Insert_Texture(replacementTexture.FileName, i);
                }
                else
                {
                    Console.WriteLine($"WARNING: No file for texture {textureName} (texture will not be replaced in model)");
                }
            }

            // Note: I took this from Ohana's save button logic, not sure the
            // exact case of the "else if"
            if (Common.MyOhana.Current_Texture != null)
            {
                File.Delete(Common.MyOhana.Current_Texture);
                File.Copy(Common.MyOhana.Temp_Texture_File, Common.MyOhana.Current_Texture);
            }
            else if (Common.MyOhana.BCH_Have_Textures)
            {
                File.Delete(Common.MyOhana.Current_Model);
                File.Copy(Common.MyOhana.Temp_Model_File, Common.MyOhana.Current_Model);
            }
        }
    }
}
