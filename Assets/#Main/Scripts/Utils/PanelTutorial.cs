using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelTutorial : MonoBehaviour
{
    public UnityEvent actionEnd;

    public Image[] tutorialImages; // Array of tutorial images
    public Image[] indicators; // Array of indicator images
    public Sprite indicatorActiveSprite; // Sprite for active indicator (e.g., star)
    public Sprite indicatorInactiveSprite; // Sprite for inactive indicator (e.g., dot)
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

        // Update indicators
        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].sprite = (i == currentIndex) ? indicatorActiveSprite : indicatorInactiveSprite;
        }
    }
}
