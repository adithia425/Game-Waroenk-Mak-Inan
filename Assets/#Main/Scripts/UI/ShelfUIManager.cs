using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfUIManager : MonoBehaviour
{
    public ShelfUI[] listShelfUI;

    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        int i = 0;
        foreach (ShelfUI item in listShelfUI)
        {
            item.SetIndex(i);
            item.RefreshText();
            i++;
        }
    }
}
