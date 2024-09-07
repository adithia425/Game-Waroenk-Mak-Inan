using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIChoice : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI textPertanyaan;
    public TextMeshProUGUI textChoice1;
    public TextMeshProUGUI textChoice2;

    public Button buttonChoice1;
    public Button buttonChoice2;

    public void SetTextUI(string pertanyaan, string choice1, string choice2)
    {
        textPertanyaan.text = pertanyaan;
        textChoice1.text = choice1;
        textChoice2.text = choice2;

        buttonChoice1.interactable = true;
        buttonChoice2.interactable = true;
    }

    public void SetTextKeterangan(string val)
    {
        textPertanyaan.text = val;
    }
}
