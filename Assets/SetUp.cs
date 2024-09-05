using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Valid game folder!");
    }

    
}
