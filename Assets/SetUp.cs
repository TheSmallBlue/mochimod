using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Narod.SteamGameFinder;
using System.IO;

public class SetUp : MonoBehaviour
{
    void Start()
    {
        // TODO: save/load file path

        if(IsInstalledOnSteam(out string path))
            ModFolderCheck(path);
        else
            CreatePathCheckPopup("<b>Steam directory not found!</b> Please paste the path that contains the game's EXE file below, or use the file picker to find it");
    }

    void ModFolderCheck(string gamePath)
    {
        Debug.Log("Valid game folder!");
    }

    void CreatePathCheckPopup(string text)
    {
        PopUpManager.Instance.Create<PathPopUp>()
            .SetText<PathPopUp>(text)
            .OnSubmit(pth =>
            {
                if (!HasGameExe(pth))
                    CreatePathCheckPopup("<b>Folder did not contain the game's EXE file!</b> Either you picked the wrong folder, or you renamed the game's EXE. Both of these things are bad!");
                else 
                    ModFolderCheck(pth);
            });
    }

    bool IsInstalledOnSteam(out string path)
    {
        SteamGameLocator gameLocator = new SteamGameLocator();
        path = "";

        if(!gameLocator.getIsSteamInstalled()) return false;

        try
        {
            path = gameLocator.getGameInfoByFolder("MetaWare High School (Demo)").steamGameLocation;
        }
        catch (DirectoryNotFoundException)
        {
            return false;
        }

        if(!HasGameExe(path)) return false;

        return true;
    }

    bool HasGameExe(string pathToFolder) => File.Exists(pathToFolder + "/MetaWareHighSchoolDemo.exe");
}
