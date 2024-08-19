using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioMusic;
    public AudioSource audioSFX;

    public AudioClip musicSplashScreen;


    [Header("SFX")]
    public AudioClip sfxLevelUpPop;
    public AudioClip sfxWinMiniGame;
    public AudioClip sfxLemparSendal;
    public AudioClip sfxNPCHilang;
    public AudioClip sfxPopUpNotif;
    public AudioClip sfxButtonUpgrade;

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
                audioSFX.PlayOneShot(sfxLevelUpPop);
                break;
            case SFX.WINMINIGAME:
                audioSFX.PlayOneShot(sfxWinMiniGame);
                break;
            case SFX.LEMPARSENDAL:
                audioSFX.PlayOneShot(sfxLemparSendal);
                break;
            case SFX.NPCHILANG:
                audioSFX.PlayOneShot(sfxNPCHilang);
                break;
            case SFX.POPUPNOTIF:
                audioSFX.PlayOneShot(sfxPopUpNotif);
                break;
            case SFX.BUTTONUPGRADE:
                audioSFX.PlayOneShot(sfxButtonUpgrade);
                break;
            default:
                break;
        }
    }
}

public enum SFX
{
    LEVELUPPOP,
    WINMINIGAME,
    LEMPARSENDAL,
    NPCHILANG,
    POPUPNOTIF,
    BUTTONUPGRADE
}
