using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    void Start()
    {
        // TODO: save/load file path

        GameInstallChecker.GetPath(pth => ModFolderCheck(pth));
    }

    void ModFolderCheck(string gamePath)
    {
        string modsRootPath = gamePath + "\\Mods";
        if (!Directory.Exists(modsRootPath))
            Directory.CreateDirectory(modsRootPath);
        
        var mods = Directory.GetDirectories(modsRootPath);

        foreach (var mod in mods)
        {
            Debug.Log(mod);
        }
    }
}
