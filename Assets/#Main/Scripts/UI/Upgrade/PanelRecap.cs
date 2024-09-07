using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelRecap : MonoBehaviour
{
    [Header("References")]
    public List<PanelRecapItem> listPanelItem;
    public TextMeshProUGUI textTotal;

    public List<int> listCounterMainanSold;

    private void OnEnable()
    {
        SetTextUI();
    }

    public void SetTextUI()
    {
        listCounterMainanSold = DatabaseManager.instance.listCounterMainanSold;
        ShelfManager shelfM = ShelfManager.instance;
        SaveManager saveM = SaveManager.instance;
        DatabaseManager dataM = DatabaseManager.instance;

        int levelShelf  =saveM.GetLevelShelf();
        Debug.Log("Set Recap Level" + levelShelf);
        int totalPrice = 0;

        for (int i = 0; i < listCounterMainanSold.Count; i++)
        {
            if (i < levelShelf)
            {

                int count = listCounterMainanSold[i];
                int price = shelfM.listShelfController[i].GetPriceMainan();

                listPanelItem[i].SetTextUI(
                    saveM.listDataShelf.dataShelf[i].nameMainan,
                    count,
                    (count * price),
                    dataM.GetImageMainan(i));

                totalPrice += (count * price);

                listPanelItem[i].gameObject.SetActive(true);
            }
            else
            {
                listPanelItem[i].gameObject.SetActive(false);
            }
        }

        textTotal.text = totalPrice.ToString();

        dataM.ResetListMainanSold();
    }
}
