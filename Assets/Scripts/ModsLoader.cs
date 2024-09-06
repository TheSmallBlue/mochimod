using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ModsLoader : MonoBehaviour
{
    [SerializeField] RectTransform modListParent;
    [SerializeField] ListedMod modPrefab;

    public static string GamePath;
    public static string ModsPath;

    void Start()
    {
        GameInstallChecker.GetPath(pth => 
        {
            GamePath = pth; 
            LoadMods();
        });
    }

    /// <summary>
    /// Looks for mods in the mods directory and populates the mods list with their values
    /// </summary>
    /// <param name="gamePath"> The game's folder path </param>
    public void LoadMods()
    {
        // If the modlist already has entries, we delete them!
        if(modListParent.childCount != 0)
        {
            for (int i = 0; i < modListParent.childCount; i++)
            {
                Destroy(modListParent.GetChild(i).gameObject);
            }
        }

        // Check that the mod directory exists
        // If it doesn't, we create it
        ModsPath = GamePath + "\\Mods";
        if (!Directory.Exists(ModsPath))
            Directory.CreateDirectory(ModsPath);
        
        // Loop through each folder inside the mods folder
        foreach (string modDirectory in Directory.GetDirectories(ModsPath))
        {

            // Does the folder have an info.json file?
            if (Directory.GetFiles(modDirectory).FirstOrDefault(x => x == modDirectory + "\\info.json") == null) continue;

            // Is the info.json file valid?
            ModInfo modInfo = JsonUtility.FromJson(File.ReadAllText(modDirectory + "\\info.json"), typeof(ModInfo)) as ModInfo;
            if (modInfo == null) continue;

            // Create the item on the list then!
            Instantiate(modPrefab, modListParent).SetFields(modInfo);
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ModInfo info = new ModInfo()
            {
                name = "Fart mod",
                author = "Nobody knows....",
                description = "A mod about farts, I think?"
            };

            File.WriteAllText(ModsPath + "\\info.json", JsonUtility.ToJson(info));
        }
    }
}
