using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TambahMainanUI : MonoBehaviour
{
    public TextMeshProUGUI textEtalaseTarget;

    public TextMeshProUGUI textCostUpgrade;
    public Button buttonUpgrade;

    [Header("Variable")]
    public int levelShelf;
    public int costUpgrade;

    private void OnEnable()
    {
        levelShelf = SaveManager.instance.listDataShelf.levelShelf;
        costUpgrade = SaveManager.instance.listDataShelf.dataShelf[levelShelf].priceToUnlock;
        SetText();

        if (levelShelf >= 9) buttonUpgrade.interactable = false;
    }

    public void SetText()
    {
        string name = SaveManager.instance.listDataShelf.dataShelf[levelShelf].nameMainan;

        textEtalaseTarget.text = $"Etalase {(levelShelf +1)}, {name}";

        textCostUpgrade.text = costUpgrade.ToString();
    }

    public void MainMiniGame()
    {
        if (GameManager.instance.GetCurrentMoney() >= costUpgrade)
        {
            /* GameManager.instance.AddMoney(-costUpgrade);
             SaveManager.instance.LevelUpCapacity();*/
            ScenesManager.instance.ChangeScene("MiniGame");
            //SetText();
        }
        else
        {
            GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
        }
    }
}
