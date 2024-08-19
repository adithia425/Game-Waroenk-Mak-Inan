using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMiniGame : MonoBehaviour
{
    public MiniGameManager miniGameManager;
    public Mainan typeMainan;
    public Image imageMainan;

    public Sprite spriteTransparant;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Clicked();
        });
    }

    public void SetMainan(Mainan mainan, Sprite sprite)
    {
        typeMainan = mainan;
        imageMainan.sprite = sprite;
    }

    public void Clicked()
    {
        if (!miniGameManager.playerCanClick) return;
        miniGameManager.PlayerClicked();

        HideImage();
        if(miniGameManager.targetMainan == typeMainan)
        {
            //Random, benar
            miniGameManager.PlayerCorrect();
        }
        else
        {
            //Penalty
        }
    }

    public void HideImage()
    {
        imageMainan.sprite = spriteTransparant;
    }
}
