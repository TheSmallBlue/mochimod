using System.Collections;
using System.Collections.Generic;
using System.IO;
using CreateMaps;
using UnityEngine;

[RequireComponent(typeof(ModsLoader))]
public class ModPlayer : MonoBehaviour
{
    public void PlayMod(ListedMod mod)
    {
        if(!Directory.Exists(ModsLoader.GamePath + "\\game_base\\")) 
            Directory.Move(ModsLoader.GamePath + "\\game\\", ModsLoader.GamePath + "\\game_base\\");

        if(JunctionPoint.Exists(ModsLoader.GamePath + "\\game\\"))
            JunctionPoint.Delete(ModsLoader.GamePath + "\\game\\");

        JunctionPoint.Create(ModsLoader.GamePath + "\\game\\", mod.ModPath + "\\game\\", true);

        System.Diagnostics.Process.Start(ModsLoader.GamePath + "\\MetaWareHighSchoolDemo.exe");
    }

    public void PlayBaseGame()
    {
        if (JunctionPoint.Exists(ModsLoader.GamePath + "\\game\\"))
            JunctionPoint.Delete(ModsLoader.GamePath + "\\game\\");
            
        if (Directory.Exists(ModsLoader.GamePath + "\\game_base\\"))
            Directory.Move(ModsLoader.GamePath + "\\game_base\\", ModsLoader.GamePath + "\\game\\");

        System.Diagnostics.Process.Start(ModsLoader.GamePath + "\\MetaWareHighSchoolDemo.exe");
    }
}
