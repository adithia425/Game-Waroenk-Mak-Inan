using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasMiniGame : MonoBehaviour
{
    public Slider sliderTimer;


    public GameObject panelTutorial;
    public GameObject panelDone;
    public GameObject panelTimeEnd;

    public GameObject panelTimer;
    void Start()
    {
        
    }

    public void SetTimer(float maxValue)
    {
        sliderTimer.maxValue = maxValue;
        sliderTimer.value = 0;
    }

    public void UpdateTimer(float val)
    {
        sliderTimer.value = val;
    }

    public void PanelTutorial(bool con)
    {
        panelTutorial.SetActive(con);
    }
    public void PanelDone(bool con)
    {
        panelDone.SetActive(con);
    }

    public void PanelTimeEnd(bool con)
    {
        panelTimeEnd.SetActive(con);
    }

    public void PanelTimer(bool con)
    {
        panelTimer.SetActive(con);
    }
}
