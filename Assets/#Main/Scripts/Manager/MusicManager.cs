using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioMusic;
    public AudioSource audioSFX;

    public AudioClip musicSplashScreen;


    [Header("UI")]
    public AudioClip sfxLevelUpPop;
    public AudioClip sfxWinMiniGame;
    public AudioClip sfxPopUpNotif;

    [Header("Event")]
    public AudioClip sfxGetMoney;
    public AudioClip sfxOpen;
    public AudioClip sfxClose;
    public AudioClip sfxFast;

    [Header("Object")]
    public AudioClip sfxLemparSendal;
    public AudioClip sfxNPCHilang;


    [Header("Button")]
    public AudioClip sfxButtonUpgrade;
    public AudioClip sfxButtonClick;
    public AudioClip sfxButtonCancel;
    public AudioClip sfxMiniGameCorrect;
    public AudioClip sfxMiniGameWrong;
    public AudioClip sfxMiniGameSE;



    [Header("MiniGame")]
    public AudioClip sfx321gas;
    public AudioClip sfxswipe;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            SetVolume(savedVolume);
        }
        else
        {
            float savedVolume = .8f;
            SetVolume(savedVolume);
        }
    }

    public void SetVolume(float val)
    {
        audioMusic.volume = val;
        audioSFX.volume = val;

        PlayerPrefs.SetFloat("Volume", val);
        PlayerPrefs.Save();
    }
    public void PlayMusicSplashScreen()
    {
        audioMusic.clip = musicSplashScreen;
        audioMusic.Play();
    }

    public void PlaySFX(SFX sfx)
    {
        switch (sfx)
        {
            case SFX.LEVELUPPOP:
                PlayNowSFX(sfxLevelUpPop);
                break;
            case SFX.DONEMINIGAME:
                PlayNowSFX(sfxWinMiniGame);
                break;
            case SFX.LEMPARSENDAL:
                PlayNowSFX(sfxLemparSendal);
                break;
            case SFX.NPCHILANG:
                PlayNowSFX(sfxNPCHilang);
                break;
            case SFX.POPUPNOTIF:
                PlayNowSFX(sfxPopUpNotif);
                break;

            case SFX.BUTTONUPGRADE:
                PlayNowSFX(sfxButtonUpgrade);
                break;
            case SFX.BUTTONCLICK:
                PlayNowSFX(sfxButtonClick);
                break;
            case SFX.BUTTONCANCEL:
                PlayNowSFX(sfxButtonCancel);
                break;


            case SFX.SFXMGCORRECT:
                PlayNowSFX(sfxMiniGameCorrect);
                break;
            case SFX.SFXMGWRONG:
                PlayNowSFX(sfxMiniGameWrong);
                break;
            case SFX.SFXMGSE:
                PlayNowSFX(sfxMiniGameSE);
                break;


            case SFX.GETMONEY:
                PlayNowSFX(sfxGetMoney);
                break;
            case SFX.OPENSHOP:
                PlayNowSFX(sfxOpen);
                break;
            case SFX.CLOSESHOP:
                PlayNowSFX(sfxClose);
                break;
            case SFX.FASTFORWARD:
                PlayNowSFX(sfxFast);
                break;


            case SFX.MG321:
                PlayNowSFX(sfx321gas);
                break;
            case SFX.MGSWIPE:
                PlayNowSFX(sfxswipe);
                break;
            default:
                break;
        }
    }

    public void PlayNowSFX(AudioClip clip)
    {
        audioSFX.PlayOneShot(clip);
    }
}

public enum SFX
{
    LEVELUPPOP,
    DONEMINIGAME,
    LEMPARSENDAL,
    NPCHILANG,
    POPUPNOTIF,
    BUTTONUPGRADE,
    BUTTONCLICK,
    BUTTONCANCEL,

    SFXMGCORRECT,
    SFXMGWRONG,
    SFXMGSE,

    GETMONEY,
    OPENSHOP,
    CLOSESHOP,
    FASTFORWARD,

    MG321,
    MGSWIPE
}
