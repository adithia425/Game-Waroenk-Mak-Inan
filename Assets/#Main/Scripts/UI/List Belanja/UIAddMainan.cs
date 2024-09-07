using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIAddMainan : MonoBehaviour
{
    [Header("References")]
    public Button buttonMin;
    public Button buttonAdd;
    public Image imageIcon;
    public TextMeshProUGUI textCounter;

    [Header("Variables")]
    public int indexMainan;
    private int incrementMainan;

    [Header("Utils")]
    public UnityEvent<int> onMinMainan;
    public UnityEvent<int> onAddMainan;
    void Start()
    {
        incrementMainan = DatabaseManager.instance.counterCollectedStockMainan;

        buttonMin.onClick.AddListener(ButtonMinMainan);
        buttonAdd.onClick.AddListener(ButtonAddMainan);
    }

    public void ButtonMinMainan()
    {
        onMinMainan?.Invoke(indexMainan);
    }
    public void ButtonAddMainan()
    {
        onAddMainan?.Invoke(indexMainan);
    }

    public void RefreshText(int count)
    {
        textCounter.text = (count * incrementMainan).ToString();
    }
}
