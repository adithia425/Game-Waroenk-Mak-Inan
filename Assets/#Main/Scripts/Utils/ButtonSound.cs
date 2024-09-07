using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public SFX sfx;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.instance.PlaySFX(sfx);
        });
    }
}
