# Dyson Sphere Program Modding

This is a small guide to develop mods for Dyson Sphere Program using Unity Mod Manager (UMM).

## Requirements
You will need a couple things before you get started:
- [Dyson Sphere Program](https://store.steampowered.com/app/1366540/Dyson_Sphere_Program/)
- [Unity Mod Manager](https://www.nexusmods.com/site/mods/21)
- `UnityModManagerConfig.xml` In this repository
- (Basic) C# knowledge

## Setting up Unity Mod Manager
After you have extracted Unit Mod Manager, open the folder that you extracted it to and fine the `UnityModManagerConfig.xml` file. We want to replace this file with our version of `UnityModManager.xml` (You can download it from this repository). This way Unity Mod Manager will be able to detect Dyson Sphere Program and it's game folder / files.

This step will not be needed if the creator of UMM had added Dyson Sphere Program to its games, which will happen soon.

After we have done this, open up `UnityModManager.exe` and select Dyson Sphere Program from the games list. The folder should be selected automaticaly, but if not: Browse to you steam installation folder, then select the folder `steamapps\common\Dyson Sphere Program`.Finally press the install button in UMM.

## Creating your first mod
After you installed Unity Mod Manager, you finaly create your first mod. Please note modding will be very hard if you have never programmed (C#) anything before, and having a basic knowledge about Unity should be helpful too.

If you don't have an IDE yet, be sure to download one for editing your code.
_[Rider](https://www.jetbrains.com/rider/), [Visual Studio](https://visualstudio.microsoft.com/), [Monodevelop](https://www.monodevelop.com/)_

Create a new project and class, something like this:
```cs
namespace DSPTest {
  static class Main {
    // Nothing here yet
  }
}
```
In this tutorial I named my mod DSPTest.

Before we are going to write some code, we must first reference some assembly references. (For most IDE's you can right click your project and click _Add reference_)
The directory we will be finding the assemblies in will be `%STEAMINSTALLPATH%\steamapps\common\Dyson Sphere Program\DSPGAME_Data\Managed`, from there add the folowing references: `Assembly-CSharp.dll`, `Assembly-CSharp-firstpass.dll`, `UnityEngine.dll`, `UnityEngine.UI.dll`,`UnityEngine.CoreModule.dll`, `UnityEngine.IMGUIModule.dll`
Now move to the UnityModManager directory and add: `UnityModManager.dll`, `0Harmony.dll`

Those are all the references you need, unless you want to use more Unity functions, which are not referenced in Unity's core.

Now we cant start coding, first we will create our loading function

```cs
// Our loading function
static bool Load(UnityModManager.ModEntry modEntry) {
  modEntry.Logger.Log("Loaded our mod!");
  // Load function must return true
  return true;
}
```

This just logs to the Unity Mod Manager log, we can also do some other things like
Add a cube:
```cs
GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
cube.transform.position = new Vector3(0, 0.5f, 0);
```
Speed up time (not the game ticks):
```cs
Time.timeScale = 10.0f
```
Show XConsole:
```cs
var console = new GameObject().AddComponent<XConsole>();
console.showConsole = true;
```

Now we want to play our mod, so lets go to `%STEAMINSTALLPATH%\steamapps\common\Dyson Sphere Program\Mods` and create a folder for you mod, the name doesn't really matter but to keep things clean you want to just name it after you mod. Now create a file called `Info.json` in this folder, this will register your mod to the mod manager. We will put the following things in the file:
```json
{
  "Id": "DSPTest",
  "Version": "1.0.0",
  "EntryMethod": "DSPTest.Main.Load"
}
```
This is the most basic configuration, make sure the mod Id matches your mod's namespace and assembly name.
For a more complex configuration:
```json
{
  "Id": "DSPTest",
  "DisplayName": "Dyson Sphere Program Test",
  "Author": "TrizlyBear",
  "Version": "1.0.0",
  "ManagerVersion": "1.0.0",
  "GameVersion": "1.0.0",
  "Requirements": [],
  "LoadAfter": [],
  "AssemblyName": "DSPTest.dll",
  "EntryMethod": "DSPTest.Main.Load"
}
```
Note: `Info.json` is case sensitive.

Now we just want to compile the mod, so go back to your IDE, and compile the mod. Your IDE will compile it to `\bin\Debug\`, but you probably can also compile it directly to your mod folder. You can change this in project settings, or open up your `csproj` file located in your project directory.
Then change
```xml
<OutputPath>bin\Debug\</OutputPath>
```
or
```xml
<OutputPath>bin\Release\</OutputPath>
```
To your desired output folder.

Our main class should look something like this now.

```cs
namespace DSPTest {
  static class Main {
    // Our loading function
    static bool Load(UnityModManager.ModEntry modEntry) {
      // Our mod is read!
      modEntry.Logger.Log("Loaded our mod!");
      
      // Create a simple cube
      GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
      cube.transform.position = new Vector3(0, 0.5f, 0);
      
      // Speed up the game
      Time.timeScale = 10.0f
      
      // Show a secret console
      var console = new GameObject().AddComponent<XConsole>();
      console.showConsole = true;
      
      // Load function must return true
      return true;
    }
  }
}
```

Now, click build in your IDE, and your mod should be ready to go

You can now launch DSP, and you will be shown a menu from menu from UMM, which can easily be opened and closed using CTRL + F10. Your mod should now be working if it shows a green dot in the UMM menu.

If you want to distribute your mod to other people so the can use it, follow these steps:
1. Open up your mod directory in `%STEAMINSTALLPATH%\steamapps\common\Dyson Sphere Program\Mods`
2. Delete all files BUT `Info.json` and your mod assembly (`YOUR_MOD_NAME.dll`).
3. Now zip the mod FOLDER and NOT just the files.

To install your mod, just open up Unity Mod Manager, and drag and drop your mod's zip file into the _Mods_ tab.

I hope this is helpfull! For any questions, problems, or mod related things, feel free to create an issue or reach out to me on Discord `TrizlyBear#7066`

### Other resources
- [dnSpy](https://github.com/dnSpy/dnSpy)
- Item and Recipe id's
- [DSP Modding discord](https://discord.gg/sgU8QfZhuB)
