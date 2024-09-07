using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEvent
{
    [Header("Data")]
    //Dijadiin 1 object
    public List<ItemDialog> listDialog;

    public string stringPertanyaan;
    public int numberAnswer; // 1 atau 2

    public string stringChoice1;
    public string stringChoice2;

    public ItemReward answerTrue;
    public ItemReward answerFalse;
}

[System.Serializable]
public class ItemDialog
{
    public string dialog;
    public string name;
    public DialogPerson dialogPerson;
    public AudioClip sfxDialog;

    public Sprite spriteMakInan;
    public Sprite spriteNPC;
}

[System.Serializable]
public class ItemReward
{
    [TextArea]
    public string stringKeteranganChoice;
    public AudioClip sfxReward;

    public int money;
    public int popularity;
    public int countShelf;
    public bool isDecrementRandomStock;
    public bool isUnlockCat;
    public bool isEventMixUhey;
    //public bool isShowCatPopUp;
    //public bool isShowThiefPopUp;
}