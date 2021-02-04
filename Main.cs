using UnityEngine;
using UnityModManagerNet;

namespace DSPTest {
  // Allows you to reload your mod if you make a change to your code and you rebuild your assembly
  [EnableReloading]
  static class Main {
    // Our loading function
    
    // Does not reset when you reload your mod
    [SaveOnReload]
    pubic static bool init = false;
    
    static bool Load(UnityModManager.ModEntry modEntry) {
      if (!ini) {
        // Our mod is read!
        modEntry.Logger.Log("Loaded our mod!");

        // Create a simple cube
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);

        // Speed up the game
        Time.timeScale = 10.0f;

        // Show a secret console
        var console = new GameObject().AddComponent<XConsole>();
        console.showConsole = true;
        
        init = true;
      }
      
      // Load function must return true
      return true;
    }
  }
}
