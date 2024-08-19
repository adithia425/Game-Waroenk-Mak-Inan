using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfController : MonoBehaviour
{
    public ItemMainan mainan;

    public int indexMainan;
    public bool isUnlock;

    public int currentStok;
    public GameObject objectMainan;
    public List<GameObject> listObjectMainan;
    public List<Transform> listPosMainan;
    public Transform parentPosMainan;

    public Transform posNPC;

    [Header("Restock")]
    public bool isOnRestock;
    public float counterTimeRestock;
    public GameObject panelRestock;
    public Button buttonRestock;
    public Image imageIcon;

    public Sprite spriteStokEmpty;
    public Sprite spriteLoading;


    public void SetDataShelf(int index, int levelPrice, int levelRestock, int basePrice, int maxStok, int timeRestock)
    {
        indexMainan = index;
        mainan.levelPrice = levelPrice;
        mainan.levelTimerRestock = levelRestock;

        mainan.basePrice = basePrice;
        mainan.maxStock = maxStok;
        mainan.timerRestock = timeRestock;
    }
    public Transform GetPosNPC()
    {
        return posNPC;
    }

    public int GetPriceMainan()
    {
        return mainan.GetCurrentPrice();
    }

    void Start()
    {
        objectMainan = GameManager.instance.database.GetObjectMainan(mainan.typeMainan);
        int countStock = listPosMainan.Count;

        for (int i = 0; i < countStock; i++)
        {
            GameObject objMainan = Instantiate(objectMainan, listPosMainan[i].position, listPosMainan[i].rotation, listPosMainan[i].transform);
            listObjectMainan[i] = objMainan;
            objMainan.SetActive(false);
        }

        SetStock();
    }


    void Update()
    {
        if (!isUnlock) return;

        if(isOnRestock)
        {
            counterTimeRestock -= Time.deltaTime;
            if(counterTimeRestock <= 0)
            {
                isOnRestock = false;
                SetStock();
            }
        }


        //Tambahan
        panelRestock.transform.forward = Camera.main.transform.forward;
    }

    public void SetStock()
    {
        panelRestock.SetActive(false);

        if(!isUnlock)
        {
            return;
        }

        currentStok = mainan.maxStock;
        RefreshStock();
    }

    public void RefreshStock()
    {
        for (int i = 0; i < listObjectMainan.Count; i++)
        {
            if (listObjectMainan[i] == null) continue;

            if(i < currentStok)
            {
                listObjectMainan[i].SetActive(true);
            }
            else
            {
                listObjectMainan[i].SetActive(false);
            }
        }
    }

    public void Restock()
    {
        buttonRestock.interactable = false;
        imageIcon.sprite = spriteLoading;

        isOnRestock = true;
        counterTimeRestock = mainan.GetTimmerRestock();
    }

    public bool DecrementStok()
    {
        if (currentStok <= 0) return false;

        currentStok--;

        if(currentStok <= 0)
        {
            panelRestock.SetActive(true);
            buttonRestock.interactable = true;
            imageIcon.sprite = spriteStokEmpty;
        }

        //Hilangkan object Mainan di etalase
        RefreshStock();
        return true;
    }



}
