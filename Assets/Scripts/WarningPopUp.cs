using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPopUp : PopUp
{
    public override WarningPopUp SetText<WarningPopUp>(string text)
    {
        mainTextRenderer.text = text;

        return this as WarningPopUp;
    }
}
