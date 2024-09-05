using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] List<PopUp> PopUps;

    private void Awake() 
    {
        Instance = this;
    }
    
    public T Create<T>() where T : PopUp
    {
        var prefab = PopUps.OfType<T>().FirstOrDefault();

        if(prefab == null) throw new System.Exception("Pop-Up does not exist or is not set as a valid Pop-Up");

        return Instantiate(prefab, transform);
    }
}
