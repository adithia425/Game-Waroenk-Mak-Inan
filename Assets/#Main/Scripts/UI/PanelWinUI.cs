using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelWinUI : MonoBehaviour
{

    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textDesc;


    public TextMeshProUGUI textPrice;
    public TextMeshProUGUI textStok;
    public TextMeshProUGUI textTimeRestock;

    public Sprite[] listSpriteMainan;
    public Image imageMainan;
 
    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        int levelShelf = SaveManager.instance.listDataShelf.levelShelf;
        DataShelf dataMainan = SaveManager.instance.listDataShelf.dataShelf[levelShelf];

        imageMainan.sprite = listSpriteMainan[levelShelf];

        textTitle.text = dataMainan.nameMainan;
        textDesc.text = dataMainan.descMainan;

        textPrice.text = "Harga : " + dataMainan.price.ToString() + " RP";
        textStok.text = "Jumlah Stok : " + dataMainan.stok.ToString();
        textTimeRestock.text = "Waktu Re-stock : " + dataMainan.timerRestock.ToString() + " Detik";
    }
}
