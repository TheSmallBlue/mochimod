using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListedMod : MonoBehaviour
{
    [SerializeField] Text modName, modDescription, modAuthor;

    public void SetFields(ModInfo mod)
    {
        modName.text = mod.name;
        modDescription.text = mod.description;
        modAuthor.text = mod.author;

        // TODO: Image
    }
}
