using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DaftarBelanjaManager : MonoBehaviour
{

    [Header("References")]
    public List<int> listCounterMainan;
    public List<TextMeshProUGUI> listTextCounterBox;
    public List<Image> listImageBoxMainan;
    public List<UIAddMainan> listUI;
    public TextMeshProUGUI textCounterBoxMainan;

    public List<GameObject> listItemBoxChoosed;

    [Header("Stock")]
    public List<GameObject> listItemStockMainan;
    public List<TextMeshProUGUI> listTextCounterStock;

    [Header("Variables")]
    public int countBoxMainan;
    public int minCountBoxMainan;
    public int maxCountBoxMainan;

    private int levelShelf;

    void Start()
    {
        levelShelf = SaveManager.instance.GetLevelShelf();

        SetUI();
        RefreshTextCounterBox();
    }

    public void AddCounterMainan(int indexMainan)
    {
        if (countBoxMainan >= maxCountBoxMainan) return;

        listCounterMainan[indexMainan]++;
        listUI[indexMainan].RefreshText(listCounterMainan[indexMainan]);
        RefreshTextCounterBox();
    }
    public void MinCounterMainan(int indexMainan)
    {
        if (listCounterMainan[indexMainan] <= 0) return;

        listCounterMainan[indexMainan]--;
        listUI[indexMainan].RefreshText(listCounterMainan[indexMainan]);
        RefreshTextCounterBox();
    }

    public void RefreshTextCounterBox()
    {
        countBoxMainan = 0;
        for (int i = 0; i < listCounterMainan.Count; i++)
        {
            listTextCounterBox[i].text = listCounterMainan[i].ToString();
            listImageBoxMainan[i].sprite = DatabaseManager.instance.GetImageBoxNoOutline(i);
            countBoxMainan += listCounterMainan[i];
        }

        textCounterBoxMainan.text = "Total Box : " + countBoxMainan.ToString()+"/" + maxCountBoxMainan;
    }



    public void SetUI()
    {
        //UI Added Mainan
        for (int i = 0; i < listUI.Count; i++)
        {
            listUI[i].indexMainan = i;
            listUI[i].imageIcon.sprite = DatabaseManager.instance.GetImageMainan(i);

            if(i < levelShelf)
            {
                listUI[i].gameObject.SetActive(true);
            }
            else
            {
                listUI[i].gameObject.SetActive(false);
            }
        }

        //UI Target Mainan
        for (int i = 0; i < listItemBoxChoosed.Count; i++)
        {
            if (i < levelShelf)
            {
                listItemBoxChoosed[i].gameObject.SetActive(true);
            }
            else
            {
                listItemBoxChoosed[i].gameObject.SetActive(false);
            }
        }

        //UI Stock Mainan
        for (int i = 0; i < listItemStockMainan.Count; i++)
        {
            if (i < levelShelf)
            {
                listItemStockMainan[i].gameObject.SetActive(true);
            }
            else
            {
                listItemStockMainan[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < listTextCounterStock.Count; i++)
        {
            if (i < levelShelf) listTextCounterStock[i].text = $"{ShelfManager.instance.GetStockShelf(i)}";
        }
    }

    public void ButtonPlayMiniGame()
    {
        if(countBoxMainan < minCountBoxMainan)
        {
            GameManager.instance.ShowPanelPopUp("Ayolah, setidaknya pilih " + minCountBoxMainan + " stock box mainan!");
            return;
        }

        DatabaseManager.instance.countMainanChoosed = countBoxMainan;
        DatabaseManager.instance.SetListMainanMiniGame(listCounterMainan);
        ScenesManager.instance.ChangeScene("MiniGame");
    }

}
