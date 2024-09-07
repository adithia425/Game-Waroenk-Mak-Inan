using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelTutorial : MonoBehaviour
{
    public UnityEvent actionEnd;

    public Image[] tutorialImages; 
    public Button buttonPrev;
    public Button buttonNext;

    private int currentIndex = 0;

    void Start()
    {
        // Initial setup
        UpdateTutorialPanel();

        // Add listeners to buttons
        buttonPrev.onClick.AddListener(OnPrevButtonClicked);
        buttonNext.onClick.AddListener(OnNextButtonClicked);
    }

    void OnPrevButtonClicked()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateTutorialPanel();
        }
    }

    void OnNextButtonClicked()
    {
        if (currentIndex < tutorialImages.Length - 1)
        {
            currentIndex++;
            UpdateTutorialPanel();
        }
        else
        {
            actionEnd.Invoke();
            gameObject.SetActive(false);
        }
    }

    void UpdateTutorialPanel()
    {
        // Update tutorial images visibility
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            tutorialImages[i].gameObject.SetActive(i == currentIndex);
        }
    }
}
