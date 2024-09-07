using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPanelShow : MonoBehaviour
{
    public AudioClip clip;

    private void OnEnable()
    {
        MusicManager.instance.PlayNowSFX(clip);
    }

}
