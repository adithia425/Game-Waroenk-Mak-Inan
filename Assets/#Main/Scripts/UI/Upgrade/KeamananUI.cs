using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeamananUI : MonoBehaviour
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
        int sLevel = SaveManager.instance.GetLevelKeamanan();


        int upgradeCost = counterCost * sLevel;

        float valueCurrent = DatabaseManager.instance.GetEffectKeamanan(sLevel);

        textDetail.text = $"Rate kesuksesan = " + valueCurrent + "%";


        if (SaveManager.instance.CheckLevelKeamananMax())
        {
            buttonUpgrade.interactable = false;
            textCostUpgrade.text = "Max";
        }
        else
        {
            textCostUpgrade.text = upgradeCost.ToString();
        }
    }

    public void UpgradeKeamanan()
    {
        int level = SaveManager.instance.GetLevelKeamanan();
        int cost = counterCost * level;

        if (GameManager.instance.GetCurrentMoney() >= cost)
        {
            MusicManager.instance.PlaySFX(SFX.BUTTONUPGRADE);
            vfxUpgrade.SetActive(true);
            CanvasManager.instance.VFXButtonUpgrade(true);
            GameManager.instance.AddMoney(-cost);
            SaveManager.instance.LevelUpKeamanan();

            SetText();
        }
        else
        {
            GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
        }
        
    }
}
