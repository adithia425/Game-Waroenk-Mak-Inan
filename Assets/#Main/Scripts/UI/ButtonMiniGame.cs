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
    public GameObject effectSpecial;

    public GameObject effectCollectSpecial;
    public GameObject panelImage;


    [Header("Data")]
    public bool isSpecials;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Clicked();
        });
    }

    public void SetMainan(Mainan mainan, Sprite sprite, bool isSpecials)
    {
        typeMainan = mainan;
        imageMainan.sprite = sprite;
        this.isSpecials = isSpecials;


        panelImage.SetActive(true);
        effectSpecial.SetActive(isSpecials);
        effectCollectSpecial.SetActive(false);
    }

    public void Clicked()
    {
        if (!miniGameManager.playerCanClick) return;

        if (isSpecials)
        {
            MusicManager.instance.PlaySFX(SFX.SFXMGSE);
            int indexMainan = DatabaseManager.instance.GetIndexMainan(typeMainan);
            SaveManager.instance.AddStockSpecial(indexMainan - DatabaseManager.instance.countMainanInGame);

            effectSpecial.SetActive(false);
            effectCollectSpecial.SetActive(true);
        }
        else
        {
            miniGameManager.SetPlayerCantClicked();
            if (miniGameManager.targetMainan == typeMainan)
            {
                //Random, benar
                miniGameManager.PlayerCorrect();
            }
            else
            {
                MusicManager.instance.PlaySFX(SFX.SFXMGWRONG);
            }
        }

        HideImage();
    }

    public void HideImage()
    {
        panelImage.SetActive(false);
    }
}
