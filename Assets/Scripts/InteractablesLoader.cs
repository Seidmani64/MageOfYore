using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesLoader : MonoBehaviour
{
    public GameObject[] interactables;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject interactable in interactables)
        {
            if(PlayerPrefs.GetInt(interactable.name,1) > 0)
                interactable.SetActive(true);
        }
    }

}
