using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopUp : MonoBehaviour
{
    [SerializeField] protected Text mainTextRenderer;

    public abstract T SetText<T>(string text) where T : PopUp;

    public void Close()
    {
        Destroy(gameObject);
    }
}
