using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KapasitasUI : MonoBehaviour
{
    public TextMeshProUGUI textCurrentPopularity;
    public TextMeshProUGUI textDetailCurrentPopularity;

    public TextMeshProUGUI textNextPopularity;
    public TextMeshProUGUI textDetailNextPopularity;

    public TextMeshProUGUI textCostUpgrade;
    public TextMeshProUGUI textLevelPopTarget;

    [Header("Variable")]
    public int counterCost;

    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        //int sLevelPop = SaveManager.instance.GetLevelPopularity();
        int sLevelCap = SaveManager.instance.GetLevelCapacity();

        int upgradeCost = counterCost * sLevelCap;

        string sPopName = GameManager.instance.database.GetNamePopularity(sLevelCap);
        int sCountNPC = GameManager.instance.database.GetCountCapacity(sLevelCap);

        string ePopName = GameManager.instance.database.GetNamePopularity(sLevelCap + 1);
        int eCountNPC = GameManager.instance.database.GetCountCapacity(sLevelCap + 1);

        textCurrentPopularity.text = $"Lv {sLevelCap}:{sPopName}";
        textDetailCurrentPopularity.text = $"Maksimal {sCountNPC} Pembeli";


        textNextPopularity.text = $"Lv {sLevelCap + 1}:{ePopName}";
        textDetailNextPopularity.text = $"Maksimal {eCountNPC} Pembeli";

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
