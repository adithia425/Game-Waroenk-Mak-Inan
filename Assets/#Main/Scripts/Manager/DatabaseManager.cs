using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            listEnumMainanArray = (Mainan[])System.Enum.GetValues(typeof(Mainan));
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public List<GameObject> listObjectMainan;
    public List<Sprite> listImageMainan;
    public List<Sprite> listImageMainanGold;
    public List<Sprite> listImageBoxNoOutline;

    public Mainan[] listEnumMainanArray;


    public List<String> listPopularityName;
    public List<int> listKapasitasCount;
    public List<float> listKeuanganLevelValue;
    public List<int> listKeamananLevelValue;

    [Header("MiniGame")]
    public int countMainanChoosed;
    public List<int> listCounterMainanMiniGame;
    public List<int> listCounterMainanCollected;
    public List<int> listCounterMainanSold;


    [Header("Data")]
    public int counterCollectedStockMainan;
    public int countMainanInGame;

    public Sprite GetImageMainan(Mainan mainan)
    {
        int index = Array.IndexOf(Enum.GetValues(mainan.GetType()), mainan);
        return listImageMainan[index];
    }
    public Sprite GetImageMainanGold(Mainan mainan)
    {
        int index = Array.IndexOf(Enum.GetValues(mainan.GetType()), mainan);
        return listImageMainanGold[index];
    }
    public Sprite GetImageMainan(int index)
    {
        return listImageMainan[index];
    }

    public Sprite GetImageBoxNoOutline(int index)
    {
        return listImageBoxNoOutline[index];
    }

    public GameObject GetObjectMainan(Mainan mainan)
    {
        int index = Array.IndexOf(Enum.GetValues(mainan.GetType()), mainan);
        return listObjectMainan[index];
    }

    public String GetNamePopularity(int index)
    {
        return listPopularityName[index - 1];
    }

    public int GetCountCapacity(int index)
    {
        return listKapasitasCount[index - 1];
    }

    public float GetEffectKeuangan(int level)
    {
        return listKeuanganLevelValue[level - 1];
    }
    public int GetEffectKeamanan(int level)
    {
        return listKeamananLevelValue[level - 1];
    }


    public void SetListMainanMiniGame(List<int> list)
    {
        listCounterMainanMiniGame = list;
    }

    public void AddListMainanCollected(int index, int countAdded)
    {
        listCounterMainanCollected[index] += countAdded;
    }
    public void AddListMainanSold(int index)
    {
        listCounterMainanSold[index]++;
    }

    public void ResetListMainanCollected()
    {
        for (int i = 0; i < listCounterMainanCollected.Count; i++)
        {
            listCounterMainanCollected[i] = 0;
        }
    }



    public void ResetListMainanSold()
    {
        for (int i = 0; i < listCounterMainanSold.Count; i++)
        {
            listCounterMainanSold[i] = 0;
        }
    }

    public int GetIndexMainan(Mainan mainan)
    {
        return System.Array.IndexOf(listEnumMainanArray, mainan);
    }
}

public enum PlayerPrefName{
    COUNTSHELF
}
