using System.Collections;
using System.Collections.Generic;
using Narod.SteamGameFinder;
using System.IO;
using System;

public static class GameInstallChecker
{

    readonly static string folderName = "MetaWare High School (Demo)";
    readonly static string fileName = "MetaWareHighSchoolDemo.exe";

    public static void GetPath(Action<string> result)
    {
        if (IsInstalledOnSteam(out string path))
            result(path);
        else
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
                    result(pth);
            });
    }

    static bool IsInstalledOnSteam(out string path)
    {
        SteamGameLocator gameLocator = new SteamGameLocator();
        path = "";

        if (!gameLocator.getIsSteamInstalled()) return false;

        try
        {
            path = gameLocator.getGameInfoByFolder(folderName).steamGameLocation;
        }
        catch (DirectoryNotFoundException)
        {
            return false;
        }

        if (!HasGameExe(path)) return false;

        return true;
    }

    static bool HasGameExe(string pathToFolder) => File.Exists(pathToFolder + "/" + fileName);
}
