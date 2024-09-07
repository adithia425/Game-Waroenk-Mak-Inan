using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KapasitasUI : MonoBehaviour
{
    public TextMeshProUGUI textDetailCurrentPopularity;

    public TextMeshProUGUI textDetailNextPopularity;

    public TextMeshProUGUI textCostUpgrade;
    public TextMeshProUGUI textLevelPopTarget;

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
        //int sLevelPop = SaveManager.instance.GetLevelPopularity();
        int sLevelCap = SaveManager.instance.GetLevelCapacity();

        int upgradeCost = counterCost * sLevelCap;

        int sCountNPC = DatabaseManager.instance.GetCountCapacity(sLevelCap);

        int eCountNPC = DatabaseManager.instance.GetCountCapacity(sLevelCap + 1);

        textDetailCurrentPopularity.text = $"Maks. {sCountNPC} Pengunjung";


        textDetailNextPopularity.text = $"Meningkatkan jumlah pengunjung menjadi {eCountNPC}";

        textCostUpgrade.text = upgradeCost.ToString();
        textLevelPopTarget.text = "Lv. " + (sLevelCap + 1).ToString();
    }

    public void UpgradeKapasitas()
    {
        int level = SaveManager.instance.GetLevelCapacity();
        int cost = counterCost * level;

        if(SaveManager.instance.GetLevelPopularity() >= (level + 1))
        {
            if (GameManager.instance.GetCurrentMoney() >= cost)
            {
                MusicManager.instance.PlaySFX(SFX.BUTTONUPGRADE);
                vfxUpgrade.SetActive(true);
                CanvasManager.instance.VFXButtonUpgrade(true);
                GameManager.instance.AddMoney(-cost);
                SaveManager.instance.LevelUpCapacity();

                SetText();
            }
            else
            {
                GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
            }
        }
        else
        {
            GameManager.instance.ShowPanelPopUp("Level popularitas tidak mencukupi");
        }
    }
}
