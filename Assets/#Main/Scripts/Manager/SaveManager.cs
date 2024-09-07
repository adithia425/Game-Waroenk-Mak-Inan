using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;


    public DataInformation dataInformation;
    public ListDataShelf listDataShelf;


    public bool isNewGame;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("NEWPLAYER"))
        {
            NewGame();
        }
        /*        foreach (var item in listDataShelf.dataShelf)
                {
                    Debug.Log("Level Price: " + item.levelPrice);
                    Debug.Log("Level Restock: " + item.levelRestock);
                }*/
    }

    //Buat Nanti
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        NewGame();

        ScenesManager.instance.ChangeScene("MainMenu");
    }
    public void NewGame()
    {
        listDataShelf.levelShelf = 2;


        int countShelf = 9; //Paksa 9 dulu
        for (int i = 0; i < countShelf; i++)
        {
            PlayerPrefs.SetInt($"{PlayerPrefName.COUNTSHELF}{i}", 0);

            listDataShelf.dataShelf[i].stockBoxSpecial = 0;
        }
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

    /*    public void UpdateLevelRestock(int index, int val)
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
        }*/

    public int GetLevelShelf()
    {
        return listDataShelf.levelShelf;
    }
    public int GetLevelPriceIndex(int index)
    {
        return listDataShelf.dataShelf[index].levelPrice;
    }
    /*    public int GetLevelRestockIndex(int index)
        {
            return listDataShelf.dataShelf[index].levelRestock;
        }*/
    public int GetLevelPopularity()
    {
        return dataInformation.levelPopularity;
    }
    public int GetLevelCapacity()
    {
        return dataInformation.levelCapacity;
    }
    public int GetLevelKeuangan()
    {
        return dataInformation.levelKeuangan;
    }
    public bool CheckLevelKeuanganMax()
    {
        return GetLevelKeuangan() >= dataInformation.maxLevelKeuangan;
    }
    public int GetLevelKeamanan()
    {
        return dataInformation.levelKeamanan;
    }
    public bool CheckLevelKeamananMax()
    {
        return GetLevelKeamanan() >= dataInformation.maxLevelKeamanan;
    }

    public bool GetSpecialActive(Mainan mainan)
    {
        int index = DatabaseManager.instance.GetIndexMainan(mainan);
        return GetSpecialActive(index);
    }
    public bool GetSpecialActive(int index)
    {
        return listDataShelf.dataShelf[index].isSpecialActive;
    }

    public int GetDay()
    {
        return dataInformation.counterDay;
    }

    public void SetLevelShelf(int val)
    {
        listDataShelf.levelShelf = val;
    }
    public void SetLevelPopularity(int val)
    {
        dataInformation.levelPopularity = val;
    }

    public void SetSpecialActiveMainan(int index, bool val)
    {
        listDataShelf.dataShelf[index].isSpecialActive = val;
    }


    public void LevelUpShelf()
    {
        int currentLevel = GetLevelShelf();
        currentLevel++;

        if (currentLevel > 8) currentLevel = 8; //paksa maks level 9

        SetLevelShelf(currentLevel);
    }
    public void LevelUpPopularity()
    {
        if(dataInformation.levelPopularity <= dataInformation.maxLevelPopularity)
            dataInformation.levelPopularity++;
    }
    public void LevelDownPopularity()
    {
        if (dataInformation.levelPopularity > 1)
            dataInformation.levelPopularity--;
    }
    public void LevelUpCapacity()
    {
        if (dataInformation.levelCapacity <= dataInformation.maxLevelCapacity)
            dataInformation.levelCapacity++;
    }
    public void LevelUpKeuangan()
    {
        if (dataInformation.levelKeuangan <= dataInformation.maxLevelKeuangan)
            dataInformation.levelKeuangan++;
    }
    public void LevelUpKeamanan()
    {
        if (dataInformation.levelKeamanan <= dataInformation.maxLevelKeamanan)
            dataInformation.levelKeamanan++;
    }
    public void AddDay()
    {
        dataInformation.counterDay++;
    }
    public void AddMoney(int val)
    {
        dataInformation.money += val;
    }

    public void AddStockSpecial(int index)
    {
        listDataShelf.dataShelf[index].stockBoxSpecial++;
    }


    public void OnAddMoney(EventChoiceMessage message)
    {
        if(message.money != 0)
        {
            Debug.Log("Event, Add Money : " + message.money);
            AddMoney(message.money);
        }
    }

    public void OnAddPopularitas(EventChoiceMessage message)
    {
        if(message.popularity != 0)
        {
            Debug.Log("Event, Add Popularity : " + message.popularity);
            GameManager.instance.AddPopularity(message.popularity);
        }
    }

    public void OnUnlockNewShelf(EventChoiceMessage message)
    {
        if(message.countShelf != 0)
        {
            for (int i = 0; i < message.countShelf; i++)
            {
                LevelUpShelf();
            }
        }
    }

    public void OnEventMixUhey(EventChoiceMessage message)
    {
        if (message.isEventMixUhey)
        {
            dataInformation.eventMajorMixUhey = true;
        }
    }

    public void OnUnlockCat(EventChoiceMessage message)
    {
        if (message.isUnlockCat)
        {
            dataInformation.eventMajorKucingMalang = true;
            GameManager.instance.catManager.CheckActivatedCat();
        }
    }


    public void ResetDay()
    {
        foreach (DataShelf item in listDataShelf.dataShelf)
        {
            item.isSpecialActive = false;
        }
    }
}
