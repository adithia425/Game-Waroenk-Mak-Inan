using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMainan : MonoBehaviour
{
    public Mainan typeMainan;

    [HideInInspector]
    public int basePrice;

    public int levelPrice;
    public int maxLevelPrice;


    [HideInInspector]
    public int timerRestock;
    public int levelTimerRestock;
    public int maxLevelTimerRestock;

    [HideInInspector]
    public int maxStock;

    public int costPrice;
    public int upgradePrice;

    public int costTimerRestock;
    public int upgradeTimerRestock;

    public int GetCurrentPrice()
    {
        return basePrice * levelPrice;
    }
    public int GetNextPrice()
    {
        return basePrice * (levelPrice + 1);
    }
    public float GetTimmerRestock()
    {
        float finalTime = timerRestock;
        for (int i = 0; i < levelTimerRestock - 1; i++)
        {
            finalTime *= 0.5f;
        }

        return finalTime;
    }
    public float GetNextTimmerRestock()
    {
        float finalTime = timerRestock;
        for (int i = 0; i < levelTimerRestock; i++)
        {
            finalTime *= 0.5f;
        }

        return finalTime;
    }

    public int GetUpgradePriceCost()
    {
        //return GetCurrentPrice() * costPrice;
        return basePrice * (levelPrice + 1) * costPrice;
    }
    public int GetUpgradeTimerRestockCost()
    {
        return levelTimerRestock * costTimerRestock;
    }

    public void UpgradeLevelPrice()
    {
        levelPrice++;
    }
    public void UpgradeLevelTimerRestock()
    {
        levelTimerRestock++;
    }

    public bool isPriceLevelMax()
    {
        return levelPrice >= maxLevelPrice;
    }

    public bool isTimerRestockLevelMax()
    {
        return levelTimerRestock >= maxLevelTimerRestock;
    }
/*    public bool isTimerRestockLevelMax()
    {
        return GetTimmerRestock() == 1;
    }*/
}

public enum Mainan
{ 
    STIK_ESKRIM,
    LEM_GELEMBUNG,
    KINCIR_ANGIN,
    KARTU_TOS,
    KELERENG,
    KERTAS_BINDER,
    BEKEL,
    GASING,
    LAYANGAN
}
