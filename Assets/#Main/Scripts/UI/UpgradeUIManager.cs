using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    public ShelfUI[] listShelfUI;
    public SpecialUI[] listSpecialUI;

    public void SetUpShelfUI(int index, bool isUnlock)
    {
        listShelfUI[index].SetIndex(index);
        listShelfUI[index].RefreshUI();
        listShelfUI[index].SetLockingUI(isUnlock);
    }

    public void SetUpSpecialUI(int index, bool isUnlock)
    {
        listSpecialUI[index].SetIndex(index);
        listSpecialUI[index].RefreshUI();
        listSpecialUI[index].SetLockingUI(isUnlock);
    }
}
