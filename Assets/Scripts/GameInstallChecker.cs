using System.Collections;
using System.Collections.Generic;
using Narod.SteamGameFinder;
using System.IO;
using System;
using UnityEngine;

public static class GameInstallChecker
{
    readonly static string folderName = "MetaWare High School (Demo)";
    readonly static string fileName = "MetaWareHighSchoolDemo.exe";

    /// <summary>
    /// Calls the delegate with the path of the folder that contains the Game's EXE file
    /// </summary>
    /// <param name="result"> The resulting path </param>
    public static void GetPath(Action<string> result)
    {
        // Check if we have the path already saved
        // If we do, check if this path has the game's EXE in it still
        if(PlayerPrefs.HasKey("GamePath") && HasGameExe(PlayerPrefs.GetString("GamePath")))
        {
            Debug.Log("Path loaded from prefs");
            result(PlayerPrefs.GetString("GamePath"));
            return;
        }

        // We don't know where the game is! We check to see if it's installed on steam
        if (IsInstalledOnSteam(out string path))
        {
            PlayerPrefs.SetString("GamePath", path); 
            result(path);
            return;
        }

        // Ok, it is not. Let's just ask the user.
        CreatePathCheckPopup("<b>Steam directory not found!</b> Please paste the path that contains the game's EXE file below, or use the file picker to find it", result);
    }

    static void CreatePathCheckPopup(string text, Action<string> result)
    {
        PopUpManager.Instance.Create<PathPopUp>()
            .SetText<PathPopUp>(text)
            .OnSubmit(pth =>
            {
                if (!HasGameExe(pth))
                    CreatePathCheckPopup("<b>Folder did not contain the game's EXE file!</b> Either you picked the wrong folder, or you renamed the game's EXE. Both of these things are bad!", result);
                else
                {
                    PlayerPrefs.SetString("GamePath", pth); 
                    result(pth); 
                }
            });
    }

    static bool IsInstalledOnSteam(out string path)
    {
        SteamGameLocator gameLocator = new SteamGameLocator();
        path = "";

        // If Steam isn't installed then there's a big, huge chance that the game is not installed on Steam.
        if (!gameLocator.getIsSteamInstalled()) return false;

        // Steam is installed! Let's check if the game's folder exists, meaning the game SHOULD be installed
        try
        { path = gameLocator.getGameInfoByFolder(folderName).steamGameLocation; }
        catch (DirectoryNotFoundException)
        { return false; }

        // Final step; does the Game's folder actually contain the game
        if (!HasGameExe(path)) return false;

        return true;
    }

    static bool HasGameExe(string pathToFolder) => File.Exists(pathToFolder + "/" + fileName);
}
