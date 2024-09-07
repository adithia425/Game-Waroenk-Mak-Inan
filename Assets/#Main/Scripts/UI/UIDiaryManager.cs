using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDiaryManager : MonoBehaviour
{
    [Header("References")]
    public Sprite spriteButtonNormal;
    public Sprite spriteButtonPressed;
    public List<Button> listButton;
    public TextMeshProUGUI textDetail;

    [Header("Variable")]
    public int counterIndex;
    [TextArea]
    public List<string> listString;
    void Start()
    {
        counterIndex = 0;
        SetUIView();
    }

    public void SetIndex(int num)
    {
        counterIndex = num;
        SetUIView();
    }

    public void SetUIView()
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            if (i == counterIndex)
            {
                listButton[i].image.sprite = spriteButtonPressed;
            }
            else
            {
                listButton[i].image.sprite = spriteButtonNormal;
            }
        }

        textDetail.text = listString[counterIndex];
    }
}
