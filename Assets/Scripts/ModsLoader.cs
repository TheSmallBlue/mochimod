using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(ModPlayer))]
public class ModsLoader : MonoBehaviour
{
    [FormerlySerializedAs("modListParent")]
    public RectTransform ModListParent;
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
        if(ModListParent.childCount > 1)
        {
            for (int i = 1; i < ModListParent.childCount; i++)
            {
                Destroy(ModListParent.GetChild(i).gameObject);
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
            // Does the folder have a game sub-folder?
            if(!Directory.Exists(modDirectory + "\\game")) continue;

            // Does the folder have an info.json file?
            if (Directory.GetFiles(modDirectory).FirstOrDefault(x => x == modDirectory + "\\info.json") == null) continue;

            // Is the info.json file valid?
            ModInfo modInfo = JsonUtility.FromJson(File.ReadAllText(modDirectory + "\\info.json"), typeof(ModInfo)) as ModInfo;
            if (modInfo == null) continue;

            // Create the item on the list then!
            var newItem = Instantiate(modPrefab, ModListParent);
            newItem.SetFields(modInfo);
            newItem.SetPath(modDirectory);
            newItem.GetComponent<Button>().onClick.AddListener( () => GetComponent<ModPlayer>().PlayMod(newItem) );
        }
    }
}
