using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListedMod : MonoBehaviour
{
    [SerializeField] Text modName, modDescription, modAuthor;

    public string ModPath { get; private set; }

    public void SetFields(ModInfo mod)
    {
        modName.text = mod.name;
        modDescription.text = mod.description;
        modAuthor.text = "by " + mod.author;

        // TODO: Image
    }

    public void SetPath(string path)
    {
        ModPath = path;
    }
}
