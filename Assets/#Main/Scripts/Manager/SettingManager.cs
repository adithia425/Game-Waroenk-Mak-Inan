using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Slider volumeSlider;

    public Button[] listButtonQuality;

    public Sprite spriteButtonNormal;
    public Sprite spriteButtonPressed;
    void Start()
    {
        CheckQuality();
        volumeSlider.onValueChanged.AddListener(SetVolume);

        RefreshSetting();
    }

    public void CheckQuality()
    {
        if (PlayerPrefs.HasKey("Quality"))
        {
            int val = PlayerPrefs.GetInt("Quality");
            SetQuality(val);
        }
        else
        {
            int val = 2;
            SetQuality(val);

            MusicManager.instance.SetVolume(0.3f);
        }
    }
    public void RefreshSetting()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SetVolume(float volume)
    {
        MusicManager.instance.SetVolume(volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);

        SetButtonQuality(qualityIndex);
    }

    public void SetButtonQuality(int index)
    {
        for (int i = 0; i < listButtonQuality.Length; i++)
        {
            if(i == index)
            {
                listButtonQuality[i].image.sprite = spriteButtonPressed;
            }
            else
            {
                listButtonQuality[i].image.sprite = spriteButtonNormal;
            }
        }
    }
}
