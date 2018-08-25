# NOTE: I am **NOT supporting** this tool!

This is a passion project hack that only modified the tool to take it as far as I am personally interested. Please do not file bugs related to "Ohana3DS" features that weren't mine to begin with. If, however, you are a developer sort and want to make this even better, I'm interested!

This is being released partly as a curiosity, partly for preservation, and maybe, just maybe, for someone else with similar interests to find useful.

## **Ohana3DS - Auto-Reload Hack by Southbird**

### **What is Ohana3DS "Rebirth"?**

The original Ohana3DS was written in Visual Basic, The Rebirth edition is a reboot of the tool in C#. See [the original Ohana3DS Rebirth](https://github.com/gdkchan/Ohana3DS-Rebirth) for more info.

### **What is the "Auto-Reload Hack"?**

**GOAL:** In the name of texture-hacking ACNL models ONLY!

Those familiar with my YouTube videos have seen texture-hacked models in Animal Crossing: New Leaf. I was annoyed that online guides told me I needed to use "new" Ohana3DS to export textures and "old" Ohana3DS to import them. I thought, why not make them into one tool?

**Please note, this was ONLY tested with character models from ACNL.** It may work for other games that "old" Ohana3DS supported.

**Technical:** What I did was take the source code to "Ohana3DS Rebirth" and partially combined it with the source code to the original "Ohana3DS", as the latter provided the texture editing support I wanted. Included in this source tree is an entire VB .NET -> C# conversion of the original Ohana3DS source code, contained in "Ohana3DS.ConvertedToC#". (This is NOT used directly by my tool, only included for reference.) If you make a Solution (sln) file to include just the Project (Ohana3DS.Converted.csproj) it will run on its own presumably (almost?) as well as the original "Ohana3DS." However, once again, I didn't really test this, it was just a step in the process. It's included in the hopes it may be useful to someone but does not actually pertain to the functionality I've added to "Ohana3DS Rebirth."

What's included (over the base "Ohana3DS Rebirth" tree) is the following:

- Ohana3DS.ConvertedToC# -- the original Ohana3DS converted from VB .NET to C# using an automated tool. Not heavily tested but may replicate the original Ohana3DS reasonably accurately.

- OhanaTextureLib -- this is an extraction of a small part of the original Ohana3DS code, mainly to support my purposes of texture importing. There's been a few minor modifications to get it to jive with "Ohana3DS Rebirth" as well as adding missing support for writing L4A4 textures (not heavily tested.)

- OhanaTexture -- A command line tool that can do texture importing/exporting. Not particularly well tested, it was an earlier path I was going down before implementing the "Auto-Reload Hack." But it "should" work if a command line version of texture importing interests you.

**Features:** Okay, so let's get into what I've actually done here!

This adds an automated texture import feature to "Ohana3DS Rebirth" (again, ONLY TESTED WITH "Animal Crossing: New Leaf" MODELS.)

You can open a textured model file (bcres in my case) as always, and now you'll find a new "File" menu option, "Set Texture Monitor Folder." What this will do is point the tool to a folder on your computer which it will "monitor" for changes and then attempt to automatically apply them to the model. (Note that there are restrictions in some cases and not all model texture formats may be supported.)

Here's the step-by-step:

1. Open a model file with textures (e.g. ACNL villager "bcres")

2. Under the "Textures" section of the UI, click the "Export" button and export all textures to a folder (I recommend a newly created empty folder!)

3. Click "File" -> "Set Texture Monitor Folder" and navigate to the folder where you've dumped the textures

Now any changes you make to the PNGs in your texture folder should automatically trigger an update and reload of the model! Note, the tool attempts to reload the model at the same position you had it previously, but sometimes gets it wrong. I haven't really looked into why and am not really experienced enough in 3D programming yet to know for sure anyway.

Caveats: 

- Exported textures (at least from ACNL) have a "mysterious" alpha to them. I don't know what this is and I haven't researched 3DS tech enough to explain whether it's a "feature" or a "bug" of "Ohana3DS." Removing the alpha from the texture and doing some color fixes worked well enough for my purposes, though.

- Some models (e.g. building interiors in ACNL) appear to use multiple "levels" (mipmaps perhaps?) and this code currently only seems to replace the "topmost" one, or something like that. I haven't looked into this more yet, but it's an inherited limitation.

