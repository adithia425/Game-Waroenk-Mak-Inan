using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShelfUI : MonoBehaviour
{
    public ItemMainan infoMainan;

    public int indexMainan;


    public TextMeshProUGUI textEtalase;
    public TextMeshProUGUI textTitle;
    public Image imageIcon;

    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textRestock;

    public Button buttonUpgradePrice;
    public TextMeshProUGUI textUpgradePrice;
    public TextMeshProUGUI textDescUpgradePriceCurrent;
    public TextMeshProUGUI textDescUpgradePriceNext;


    public Button buttonUpgradeRestock;
    public TextMeshProUGUI textUpgradeRestock;
    public TextMeshProUGUI textDescUpgradeRestockCurrent;
    public TextMeshProUGUI textDescUpgradeRestockNext;


    public TextMeshProUGUI textLevelPrice;
    public TextMeshProUGUI textLevelRestock;


    public void SetIndex(int index)
    {
        indexMainan = index;
    }

    public void RefreshText()
    {
        textEtalase.text = "Etalase " + (indexMainan + 1);
        textTitle.text = SaveManager.instance.listDataShelf.dataShelf[indexMainan].nameMainan;
        imageIcon.sprite = GameManager.instance.database.GetImageMainan(infoMainan.typeMainan);

        textPrice.text = infoMainan.GetCurrentPrice().ToString();

        float timerCurrentRestock = infoMainan.GetTimmerRestock();
        if (timerCurrentRestock % 1 == 0)
        {
            textRestock.text = timerCurrentRestock.ToString("F0");
        }
        else
        {
            textRestock.text = timerCurrentRestock.ToString("F1");
        }

        bool isLevelMaxPrice = infoMainan.isPriceLevelMax();
        buttonUpgradePrice.interactable = !isLevelMaxPrice;

        if (isLevelMaxPrice)
        {
            //textUpgradePrice.text = "Level Max";
            //textDescUpgradePriceCurrent.text = infoMainan.GetCurrentPrice().ToString();
        }
        else
        {
            textUpgradePrice.text = infoMainan.GetUpgradePriceCost().ToString();
            textDescUpgradePriceCurrent.text = infoMainan.GetCurrentPrice().ToString();
            textDescUpgradePriceNext.text = infoMainan.GetNextPrice().ToString();
        }


        bool isLevelMaxRestock = infoMainan.isTimerRestockLevelMax();
        buttonUpgradeRestock.interactable = !isLevelMaxRestock;

        if (isLevelMaxRestock)
        {
            //textUpgradeRestock.text = "Level Max";
            //textDescUpgradeRestock.text = infoMainan.GetTimmerRestock().ToString();
        }
        else
        {
            textUpgradeRestock.text = infoMainan.GetUpgradeTimerRestockCost().ToString();

            if (timerCurrentRestock % 1 == 0)
            {
                textDescUpgradeRestockCurrent.text = timerCurrentRestock.ToString("F0");
            }
            else
            {
                textDescUpgradeRestockCurrent.text = timerCurrentRestock.ToString("F1");
            }

            float timerUpgradeRestock = infoMainan.GetNextTimmerRestock();
            if (timerUpgradeRestock % 1 == 0)
            {
                textDescUpgradeRestockNext.text = timerUpgradeRestock.ToString("F0");
            }
            else
            {
                textDescUpgradeRestockNext.text = timerUpgradeRestock.ToString("F1");
            }

        }


        textLevelPrice.text = SaveManager.instance.listDataShelf.dataShelf[indexMainan].levelPrice.ToString();
        textLevelRestock.text = SaveManager.instance.listDataShelf.dataShelf[indexMainan].levelRestock.ToString();
    }

    public void UpgradePrice()
    {
        if (GameManager.instance.GetCurrentMoney() < infoMainan.GetUpgradePriceCost())
        {
            GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
            return;
        }
        GameManager.instance.AddMoney(-infoMainan.GetUpgradePriceCost());
        infoMainan.UpgradeLevelPrice();
        //Save
        int indexMainan = GameManager.instance.GetIndexMainan(infoMainan.typeMainan);
        SaveManager.instance.UpdateLevelPrice(indexMainan, infoMainan.levelPrice);
        RefreshText();
    }

    public void UpgradeTimerRestock()
    {
        if (GameManager.instance.GetCurrentMoney() < infoMainan.GetUpgradeTimerRestockCost())
        {
            GameManager.instance.ShowPanelPopUp("Jumlah uang tidak mencukupi");
            return;
        }

        GameManager.instance.AddMoney(-infoMainan.GetUpgradeTimerRestockCost());
        infoMainan.UpgradeLevelTimerRestock();
        //Save
        int indexMainan = GameManager.instance.GetIndexMainan(infoMainan.typeMainan);
        SaveManager.instance.UpdateLevelRestock(indexMainan, infoMainan.levelTimerRestock);
        RefreshText();
    }


    void Update()
    {
        
    }
}
