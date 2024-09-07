using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;

    public UpgradeUIManager uiManager;
    public List<ShelfController> listShelfController;


    private void Awake()
    {
        instance = this;
    }

    public void SetShelf()
    {
        SaveManager sm = SaveManager.instance;
        ListDataShelf listData = SaveManager.instance.listDataShelf;
        for (int i = 0; i < listData.dataShelf.Length; i++)
        {
            int countCurrentStock = PlayerPrefs.GetInt($"{PlayerPrefName.COUNTSHELF}{i}");


            listShelfController[i].SetDataShelf(i,
                listData.dataShelf[i].levelPrice, 
                listData.dataShelf[i].price,
                countCurrentStock,
                sm.GetSpecialActive(listData.dataShelf[i].mainan)
                );

            listShelfController[i].onDecrementStock.AddListener(OnDecrementStock);
        }

        int countMainan = listShelfController.Count;
        int levelShelf = SaveManager.instance.GetLevelShelf();

        //Shelf UI Upgrade
        for (int i = 0; i < countMainan; i++)
        {
            if (i < levelShelf)
            {
                //GameManager.instance.canvas.SetShelfUI(i, true);

                listShelfController[i].isUnlock = true;
                uiManager.SetUpShelfUI(i,true);
            }
            else
            {
                //GameManager.instance.canvas.SetShelfUI(i, false);

                listShelfController[i].isUnlock = false;
                uiManager.SetUpShelfUI(i, false);
            }


            listShelfController[i].SetStock();
        }

        for (int i = 0; i < countMainan; i++)
        {
            if (i < levelShelf)
            {
                uiManager.SetUpSpecialUI(i, true);
            }
            else
            {
                uiManager.SetUpSpecialUI(i, false);
            }
        }
    }

    public void SetUpStockShelf(List<int> listStockCollect)
    {
        //Debug.Log("Set up Stock");
        for (int i = 0; i < listShelfController.Count; i++)
        {
            //Debug.Log("Current Stock + " + listShelfController[i].CurrentStock + " | Increment " + listStockCollect[i]);

            listShelfController[i].CurrentStock += listStockCollect[i];
        }
    }

    public ShelfController GetShelfController(Mainan mainan)
    {
        foreach (ShelfController item in listShelfController)
        {
            if (item.mainan.typeMainan == mainan)
            {
                return item;
            }
        }

        return null;
    }

    public Transform GetPosNPCShelf(Mainan mainan)
    {
        ShelfController shelf = GetShelfController(mainan);

        if (shelf != null)
            return shelf.GetPosNPC();

        Debug.LogError("Shelf Null");
        return null;
    }

    public int GetPriceShelf(Mainan mainan)
    {
        ShelfController shelf = GetShelfController(mainan);

        //Cek jika harga 2x lipa
        bool isSpecialActive = SaveManager.instance.GetSpecialActive(mainan);


        if (shelf != null)
        {
            int price = shelf.GetPriceMainan();
            if (isSpecialActive)
            {
                price *= 2;
            }

            return price;
        }

        Debug.LogError("Shelf Null");
        return -1;
    }


    public ShelfController GetRandomShelf()
    {
        List<ShelfController> unlockedShelf = new List<ShelfController>();
        ShelfController choosedShelf;

        foreach (ShelfController item in listShelfController)
        {
            if (!item.isUnlock)
            {
                unlockedShelf.Add(item);
            }
        }

        if (unlockedShelf.Count > 0)
        {
            int randomIndex = Random.Range(0, unlockedShelf.Count);
            choosedShelf = unlockedShelf[randomIndex];

            return choosedShelf;
        }
        
        return null;
        
    }

    public List<Mainan> GetListUnlockedMainan()
    {
        List<Mainan> listMainan = new List<Mainan>();
        foreach (ShelfController item in listShelfController)
        {
            if(item.isUnlock)
            {
                listMainan.Add(item.mainan.typeMainan);
            }
        }

        return listMainan;
    }

    public int GetStockShelf(Mainan mainan)
    {
        int index = DatabaseManager.instance.GetIndexMainan(mainan);
        return GetStockShelf(index);
    }

    public int GetStockShelf(int index)
    {
        return listShelfController[index].CurrentStock;
    }

    public bool IsAllShelfStockEmpty()
    {
        bool isEmpty = true;
        for (int i = 0; i < listShelfController.Count; i++)
        {
            if(GetStockShelf(i) > 0)
            {
                isEmpty = false;
                break;
            }
        }

        return isEmpty;
    }


    public void OnDecrementStock()
    {
        if(IsAllShelfStockEmpty())
        {
            //Tutup Toko
            GameManager.instance.ClosedShop();
        }
    }

    // Event
    public void OnDecrementStockRandomMainan(EventChoiceMessage message)
    {
        for (int i = 0; i < listShelfController.Count; i++)
        {
            if (GetStockShelf(i) > 0)
            {
                if(listShelfController[i].DecrementStok())
                {
                    //Debug.Log("Event Anak Malang, Berhasil Mengurangi Mainan");
                }
                else
                {
                    //Debug.Log("Event Anak Malang, Gagal Mengurangi Mainan");
                }
                break;
            }
        }
    }
}
