using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    public DataInformation dataInformation;
    public ListDataShelf listDataShelf;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
/*        foreach (var item in listDataShelf.dataShelf)
        {
            Debug.Log("Level Price: " + item.levelPrice);
            Debug.Log("Level Restock: " + item.levelRestock);
        }*/
    }

    public void SaveGame()
    {
        dataInformation.money = GameManager.instance.GetCurrentMoney();
        dataInformation.popularity = GameManager.instance.GetCurrentPopularity();
    }

    public void UpdateLevelPrice(int index, int val)
    {
        if (index >= 0 && index < listDataShelf.dataShelf.Length)
        {
            listDataShelf.dataShelf[index].levelPrice = val;
            Debug.Log($"Updated levelPrice at index {index} to {val}");
        }
        else
        {
            Debug.LogWarning("Index out of range");
        }
    }

    public void UpdateLevelRestock(int index, int val)
    {
        if (index >= 0 && index < listDataShelf.dataShelf.Length)
        {
            listDataShelf.dataShelf[index].levelRestock = val;
            Debug.Log($"Updated levelRestock at index {index} to {val}");
        }
        else
        {
            Debug.LogWarning("Index out of range");
        }
    }

    public int GetLevelShelf()
    {
        return listDataShelf.levelShelf;
    }
    public int GetLevelPriceIndex(int index)
    {
        return listDataShelf.dataShelf[index].levelPrice;
    }
    public int GetLevelRestockIndex(int index)
    {
        return listDataShelf.dataShelf[index].levelRestock;
    }
    public int GetLevelPopularity()
    {
        return dataInformation.levelPopularity;
    }
    public int GetLevelCapacity()
    {
        return dataInformation.levelCapacity;
    }
    public void SetLevelShelf(int val)
    {
        listDataShelf.levelShelf = val;
    }
    public void SetLevelPopularity(int val)
    {
        dataInformation.levelPopularity = val;
    }

    public void LevelUpShelf()
    {
        int currentLevel = GetLevelShelf();
        currentLevel++;
        SetLevelShelf(currentLevel);
    }
    public void LevelUpPopularity()
    {
        dataInformation.levelPopularity++;
    }
    public void LevelUpCapacity()
    {
        dataInformation.levelCapacity++;
    }

    public void AddMoney(int val)
    {
        dataInformation.money += val;
    }
}
