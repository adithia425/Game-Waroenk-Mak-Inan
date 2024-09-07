using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeuanganUI : MonoBehaviour
{
    public TextMeshProUGUI textDetail;
    public Button buttonUpgrade;


    public TextMeshProUGUI textCostUpgrade;

    [Header("Variable")]
    public int counterCost;

    [Header("VFX")]
    public GameObject vfxUpgrade;
    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        int sLevel = SaveManager.instance.GetLevelKeuangan();

        int upgradeCost = counterCost * sLevel;

        float valueCurrent = DatabaseManager.instance.GetEffectKeuangan(sLevel);
        if (valueCurrent % 1 == 0) 
        {
            textDetail.text = $"Durasi NPC di kasir = {valueCurrent.ToString("F0")} Detik";
        }
        else
        {
            textDetail.text = $"Durasi NPC di kasir = {valueCurrent.ToString("F2")} Detik";
        }

        if (SaveManager.instance.CheckLevelKeuanganMax())
        {
            buttonUpgrade.interactable = false;
            textCostUpgrade.text = "Max";
        }
        else
            textCostUpgrade.text = upgradeCost.ToString();
    }

    public void UpgradeKeuangan()
    {
        int level = SaveManager.instance.GetLevelKeuangan();
        int cost = counterCost * level;

        if (GameManager.instance.GetCurrentMoney() >= cost)
        {
            MusicManager.instance.PlaySFX(SFX.BUTTONUPGRADE);
            vfxUpgrade.SetActive(true);
            CanvasManager.instance.VFXButtonUpgrade(true);
            GameManager.instance.AddMoney(-cost);
            SaveManager.instance.LevelUpKeuangan();

            SetText();
        }
        else
        {
            GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
        }
        
    }
}
