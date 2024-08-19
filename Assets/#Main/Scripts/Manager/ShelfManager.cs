using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager instance;

    public List<ShelfController> listShelfController;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        SetShelf();
    }
    public void SetShelf()
    {
        ListDataShelf listData = SaveManager.instance.listDataShelf;
        for (int i = 0; i < listData.dataShelf.Length; i++)
        {
            listShelfController[i].SetDataShelf(i,
                listData.dataShelf[i].levelPrice, 
                listData.dataShelf[i].levelRestock,
                listData.dataShelf[i].price,
                listData.dataShelf[i].stok,
                listData.dataShelf[i].timerRestock);
        }

        for (int i = 0; i < listShelfController.Count; i++)
        {
            if (i < SaveManager.instance.GetLevelShelf())
            {
                GameManager.instance.canvas.SetShelfUI(i, true);


                listShelfController[i].isUnlock = true;
                listShelfController[i].SetStock();
            }
            else
            {
                GameManager.instance.canvas.SetShelfUI(i, false);

                listShelfController[i].isUnlock = false;
            }
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

        if (shelf != null)
            return shelf.GetPriceMainan();


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


    public void UnlockShelf()
    {
        //Play MiniGame
        //SaveManager.instance.LevelUpShelf();
        //Misal berhasil unlock Shelf
        //SetShelf();

        //Pop Up??

        //Change Scene
        ScenesManager.instance.ChangeScene("MiniGame");
    }

    public void CheatUnlockShelf()
    {
        SaveManager.instance.LevelUpShelf();
        //Misal berhasil unlock Shelf
        SetShelf();
    }
}
