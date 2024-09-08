using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CreateMaps;
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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var newMod = new ModInfo()
            {
                name = "Splingus",
                author = "Dingus",
                description = "The Flingus"
            };

            File.WriteAllText(ModsLoader.ModsPath + "\\info.json", JsonUtility.ToJson(newMod));
        }
    }
}
