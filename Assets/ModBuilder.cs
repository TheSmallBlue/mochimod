using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ModBuilder : MonoBehaviour
{
    public void StartSetUp()
    {
        GameFolderSelectionPopup("Please select your mod's \"game\" folder.");
    }

    void GameFolderSelectionPopup(string text)
    {
        PopUpManager.Instance.Create<PathPopUp>()
        .SetText<PathPopUp>(text)
        .OnSubmit(pth =>
        {
            if(pth.Substring(pth.Length - 4) != "game")
            {
                GameFolderSelectionPopup("Selected folder was not the \"game\" folder. Please try again.");
            } else 
            {

            }
        });
    }

    // Make user select mod's game folder
    // Make user fill in mod information
    // Make user select mod destination
    // Create new folder in mod destination to contain mod data, named after the mod
    // Create a "game" folder in the new folder
    // Create new info.json file in new folder
    // Hash the mod's game folder
    // Compare newly hashed folder to already pre-hashed game directory folder
    // If any file is different in the mod's game folder, we copy said file to new folder's game folder
    // Result: folder with an info.json file, and a game folder that only contains modified/different files.
    // What do we do with those files? TODO.

    [DllImport("kernel32.dll")]
    static extern bool CreateSymbolicLink(
        string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

    enum SymbolicLink
    {
        File = 0,
        Directory = 1
    }


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PopUpManager.Instance.Create<PathPopUp>()
            .SetText<PathPopUp>("Hash Game")
            .OnSubmit(pth =>
            {
                var gameHash = JsonUtility.ToJson(FileHasher.HashDirectory(pth));

                PopUpManager.Instance.Create<PathPopUp>()
                .SetText<PathPopUp>("Hash Mod")
                .OnSubmit(pth2 =>
                {
                    var modHash = JsonUtility.ToJson(FileHasher.HashDirectory(pth2));

                    CompareHashes(gameHash, modHash);
                });
            });
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            PopUpManager.Instance.Create<PathPopUp>()
            .SetText<PathPopUp>("Path in which to create SymLink")
            .OnSubmit(pth =>
            {
                var symlinkPath = pth;

                PopUpManager.Instance.Create<PathPopUp>()
                .SetText<PathPopUp>("File to SymLink")
                .OnSubmit(pth2 =>
                {
                    var symlinkFile = pth2;

                    if(!CreateSymbolicLink(pth2, pth, SymbolicLink.File))
                    {
                        Debug.Log(Marshal.GetLastWin32Error());
                    }
                });
            });
        }
    }

    void CompareHashes(string gameHashJson, string modHashJson)
    {
        var gameHashDir = ((HashedDirectory)JsonUtility.FromJson(gameHashJson, typeof(HashedDirectory))).hashedFiles;
        var gameHash = gameHashDir.Select(x => x.fileHash);
        var modHashDir = ((HashedDirectory)JsonUtility.FromJson(modHashJson, typeof(HashedDirectory))).hashedFiles;
        var modHash = modHashDir.Select(x => x.fileHash);

        var hashDiffs = modHash.Except(gameHash).Distinct().ToArray();

        foreach (var diffHash in hashDiffs)
        {
            Debug.Log(diffHash);
            Debug.Log(modHashDir.FirstOrDefault(x => x.fileHash == diffHash).filePath);
        }
    }
}
