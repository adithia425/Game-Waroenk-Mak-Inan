using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelRecapItem : MonoBehaviour
{

    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textMoney;
    public Image imageIcon;

    public void SetTextUI(string name, int counter, int price, Sprite icon)
    {
        textName.text = name;
        textCounter.text = counter + "X";
        textMoney.text = price.ToString();
        imageIcon.sprite = icon;
    }
}
