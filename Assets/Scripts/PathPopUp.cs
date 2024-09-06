using System;
using System.Collections;
using System.Collections.Generic;
using SFB;
using UnityEngine;
using UnityEngine.UI;

public class PathPopUp : PopUp
{
    Action<string> submitDelegate = delegate {};

    string _path;

    public override PathPopUp SetText<PathPopUp>(string text)
    {
        mainTextRenderer.text = text;

        return this as PathPopUp;
    }

    public PathPopUp OnSubmit(Action<string> action)
    {
        submitDelegate = action;

        return this;
    }

    public void OpenFilePicker()
    {
        var result = StandaloneFileBrowser.OpenFolderPanel("Get game path", "", false);

        if(result.Length == 0) return;
        
        _path = result[0];

        GetComponentInChildren<InputField>().text = _path;
    }

    public void SetPath(string path)
    {
        _path = path;

        Debug.Log("path set to " + path);
    }

    public void Submit()
    {
        if(_path == "") return;
        
        submitDelegate(_path);

        Close();
    }
}
