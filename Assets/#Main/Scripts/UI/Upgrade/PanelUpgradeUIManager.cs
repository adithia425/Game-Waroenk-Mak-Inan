using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUpgradeUIManager : MonoBehaviour
{
    public GameObject panelFasilitas;
    public GameObject panelSDM;
    public GameObject panelSpecials;
    private void Start()
    {
        SetPanel(1);
    }


    public void SetPanel(int index)
    {
        switch (index)
        {
            case 1:
                panelFasilitas.SetActive(true);
                panelSDM.SetActive(false);
                panelSpecials.SetActive(false);
                break;
            case 2:
                panelFasilitas.SetActive(false);
                panelSDM.SetActive(true);
                panelSpecials.SetActive(false);
                break;
            case 3:
                panelFasilitas.SetActive(false);
                panelSDM.SetActive(false);
                panelSpecials.SetActive(true);
                break;
            default:
                break;
        }
    }
}
