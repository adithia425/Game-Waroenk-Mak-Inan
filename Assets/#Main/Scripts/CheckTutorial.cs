using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorial : MonoBehaviour
{

    public GameObject panelTutorial;
    void Start()
    {
        if(!PlayerPrefs.HasKey("NEWPLAYER"))
        {
            PlayerPrefs.SetInt("NEWPLAYER", 1);
            panelTutorial.SetActive(true);
        }
    }

}
