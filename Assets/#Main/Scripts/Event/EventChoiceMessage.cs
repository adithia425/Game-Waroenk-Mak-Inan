using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EventChoiceMessage
{
    public int money;
    public int popularity;
    public int countShelf;
    public bool isDecrementRandomStock;
    public bool isUnlockCat;
    public bool isEventMixUhey;

    public EventChoiceMessage(
             int money,
             int popularitas,
             int countShelf,
             bool isDecrementRandomStock,
             bool isUnlockCat,
             bool isEventMixUhey)
    {
        this.money = money;
        this.popularity = popularitas;
        this.countShelf = countShelf;
        this.isDecrementRandomStock = isDecrementRandomStock;
        this.isUnlockCat = isUnlockCat;
        this.isEventMixUhey = isEventMixUhey;
    }
}
