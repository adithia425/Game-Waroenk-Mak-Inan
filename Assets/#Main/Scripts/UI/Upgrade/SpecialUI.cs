using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialUI : MonoBehaviour
{
    public ItemMainan infoMainan;

    public int indexMainan;


    public TextMeshProUGUI textSpecial;
    public TextMeshProUGUI textTitle;
    public Image imageIcon;

    public TextMeshProUGUI textCountNeeded;

    public Button buttonUsing;

    [Header("Panel")]
    public GameObject panelUnlock;
    public GameObject panelLocked;

    public void SetIndex(int index)
    {
        indexMainan = index;
    }

    public void RefreshUI()
    {
        DataShelf dataShelf = SaveManager.instance.listDataShelf.dataShelf[indexMainan];
        textSpecial.text = "Spesial " + (indexMainan + 1);
        textTitle.text = dataShelf.nameMainan;
        imageIcon.sprite = DatabaseManager.instance.GetImageMainanGold(infoMainan.typeMainan);


        int countBoxHave = dataShelf.stockBoxSpecial;

        textCountNeeded.text = $"{countBoxHave}/{dataShelf.costConsumeBoxSpecial}";
    }

    public void ButtonConsumeSpesial()
    {
        int countBoxHave = SaveManager.instance.listDataShelf.dataShelf[indexMainan].stockBoxSpecial;
        int countBoxConsume = SaveManager.instance.listDataShelf.dataShelf[indexMainan].costConsumeBoxSpecial;

        if (countBoxHave < countBoxConsume)
        {
            GameManager.instance.ShowPanelPopUp("Jumlah bahan tidak mencukupi");
            return;
        }

        buttonUsing.interactable = false;
        SaveManager.instance.listDataShelf.dataShelf[indexMainan].stockBoxSpecial -= countBoxConsume;
        SaveManager.instance.SetSpecialActiveMainan(indexMainan, true);
        RefreshUI();
    }

    public void SetLockingUI(bool unlocking)
    {
        if (unlocking)
        {
            UnlockedUI();
        }
        else
        {
            LockedUI();
        }
    }

    private void LockedUI()
    {
        panelUnlock.SetActive(false);
        panelLocked.SetActive(true);
    }

    private void UnlockedUI()
    {
        panelUnlock.SetActive(true);
        panelLocked.SetActive(false);
    }
}
