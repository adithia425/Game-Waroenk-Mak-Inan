using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShelfController : MonoBehaviour
{
    public ItemMainan mainan;

    public int indexMainan;
    public bool isUnlock;

    [SerializeField]
    private int currentStock;
    public int CurrentStock
    {
        get
        {
            return currentStock;
        }
        set
        {
            currentStock = value;

            RefreshStock();
            PlayerPrefs.SetInt( $"{PlayerPrefName.COUNTSHELF}{indexMainan}", currentStock);


        }
    }


    public GameObject objectMainan;
    public List<GameObject> listObjectMainan;
    public List<Transform> listPosMainan;
    public Transform parentPosMainan;

    public Transform posNPC;

    [Header("Upgrade")]
    public List<Color> listColorLevel;
    public Renderer objectRenderer1;
    public Renderer objectRenderer2;


    [Header("References")]
    public GameObject panelStock;
    public TextMeshProUGUI textStock;
    public Image iconMainan;


    [Header("Event")]
    public UnityEvent onDecrementStock;


    [Header("Model")]
    public GameObject vfxSpecial;
    public GameObject vfxUpgrade;

    public void SetStock()
    {
        //panelRestock.SetActive(false);


        if (!isUnlock)
        {
            panelStock.SetActive(false);
            return;
        }

        //Ubah jadi get stock dari database manager
        //currentStok = mainan.maxStock;

        //Jika sudah unlock

        iconMainan.sprite = DatabaseManager.instance.GetImageMainan(indexMainan);
        panelStock.SetActive(true);

        //RefreshStock();
    }


    public void SetDataShelf(int index, int levelPrice, int basePrice, int currentStock, bool isSpecialActive)
    {
        indexMainan = index;
        mainan.levelPrice = levelPrice;
        objectRenderer1.material.color = listColorLevel[levelPrice - 1];
        objectRenderer2.material.color = listColorLevel[levelPrice - 1];
        //mainan.levelTimerRestock = levelRestock;

        mainan.basePrice = basePrice;
        //mainan.maxStock = maxStok;
        //mainan.timerRestock = timeRestock;

        CurrentStock = currentStock;
        //Debug.Log($"Last Stock {mainan.typeMainan} : {CurrentStock}");


        vfxSpecial.SetActive(isSpecialActive);


        iconMainan.sprite = DatabaseManager.instance.GetImageMainan(indexMainan);
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
        objectMainan = DatabaseManager.instance.GetObjectMainan(mainan.typeMainan);
        int countStock = listPosMainan.Count;

        for (int i = 0; i < countStock; i++)
        {
            GameObject objMainan = Instantiate(objectMainan, listPosMainan[i].position, listPosMainan[i].rotation, listPosMainan[i].transform);
            listObjectMainan[i] = objMainan;
            objMainan.SetActive(false);
        }

        RefreshStock();
    }


/*    void Update()
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
    }*/




    public void RefreshStock()
    {
        RefreshStockModel();
        RefreshStockUI();
    }

/*    public void Restock()
    {
        buttonRestock.interactable = false;
        imageIcon.sprite = spriteLoading;

        isOnRestock = true;
        counterTimeRestock = mainan.GetTimmerRestock();
    }*/

    public bool DecrementStok()
    {
        if (CurrentStock <= 0) return false;

        CurrentStock--;
        onDecrementStock?.Invoke();

/*        if(currentStok <= 0)
        {
            panelRestock.SetActive(true);
            buttonRestock.interactable = true;
            imageIcon.sprite = spriteStokEmpty;
        }*/

        //Hilangkan object Mainan di etalase
        //RefreshStock();
        return true;
    }


    private void RefreshStockModel()
    {
        for (int i = 0; i < listObjectMainan.Count; i++)
        {
            if (listObjectMainan[i] == null) continue;

            if (i < CurrentStock)
            {
                listObjectMainan[i].SetActive(true);
            }
            else
            {
                listObjectMainan[i].SetActive(false);
            }
        }
    }
    private void RefreshStockUI()
    {
        textStock.text = CurrentStock.ToString();
    }


    public void PlayVFXUpgrade()
    {
        vfxUpgrade.SetActive(true);
    }

}
