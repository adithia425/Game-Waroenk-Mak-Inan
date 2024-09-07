using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPrepare : MonoBehaviour
{
    public Button buttonOpen;
    void Start()
    {
        Invoke(nameof(CheckStock), .1f);
    }

    public void CheckStock()
    {
        buttonOpen.interactable = !ShelfManager.instance.IsAllShelfStockEmpty();
    }
}
