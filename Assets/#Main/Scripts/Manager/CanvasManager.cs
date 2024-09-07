using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [Header("Panel")]
    [SerializeField] private GameObject panelPrepare;
    [SerializeField] private GameObject panelDaftarBelanja;
    [SerializeField] private GameObject panelUpgrade;
    [SerializeField] private GameObject panelRecap;
    [SerializeField] private GameObject panelLoading;


/*
    [Header("VFX")]
    [SerializeField] private GameObject vfxButtonUpgrade;*/



    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textPopularity;
    public TextMeshProUGUI textNamePopularity;


    public TextMeshProUGUI textTimeDay;
    public TextMeshProUGUI textTimeDaily;

    public List<ShelfUI> listShelfUI;
    public List<SpecialUI> listSpecialUI;

    private void Awake()
    {
        instance = this;
    }

    public void PanelPrepare(bool con)
    {
        panelPrepare.SetActive(con);
    }
    public void PanelDaftarBelanja(bool con)
    {
        panelDaftarBelanja.SetActive(con);
    }
    public void PanelUpgrade(bool con)
    {
        panelUpgrade.SetActive(con);
    }
    public void PanelRecap(bool con)
    {
        panelRecap.SetActive(con);
    }

    public void PanelLoading(bool con)
    {
        panelLoading.SetActive(con);
    }

/*    public void SetShelfUI(int index, bool con)
    {
        //listShelfUI[index].gameObject.SetActive(con);
        //Atur jadi panggil funsi locked
        listShelfUI[index].SetLockingUI(con);
    }*/

    public void SetTextMoney(int val)
    {
        textMoney.text = val.ToString();
    }

    public void SetTextTime(string val)
    {
        textTimeDaily.text = val;
    }
    public void SetTextDay(string val)
    {
        textTimeDay.text = "Hari " + val;
    }

    public void SetTextPopularity(int val, int targetPopularitas)
    {
        textPopularity.text = val + "/" + targetPopularitas;
    }

    public void SetTextNamePopularity(int level)
    {
        textNamePopularity.text = "Lv. " + level + " " + DatabaseManager.instance.GetNamePopularity(level);
    }


    public void VFXButtonUpgrade(bool con)
    {
        //vfxButtonUpgrade.SetActive(true);
    }
}
