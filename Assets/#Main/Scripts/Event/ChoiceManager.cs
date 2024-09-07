using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceManager : MonoBehaviour
{
    [Header("References")]
    public UIChoice uiChoice;

    [Header("Variable")]
    public ItemEvent currentEvent;

    [Header("Event")]
    public UnityEvent<bool> onTriggerActivated;
    public UnityEvent<EventChoiceMessage> actionChoice;


    public void ActivatedChoice()
    {
        uiChoice.gameObject.SetActive(true);
    }

    public void SetEvent(ItemEvent newEvent)
    {
        currentEvent = newEvent;

        //Set text dan choice ke UI
        uiChoice.SetTextUI(
            currentEvent.stringPertanyaan, 
            currentEvent.stringChoice1, 
            currentEvent.stringChoice2
            );

        if (currentEvent.numberAnswer == 1)
        {
            uiChoice.buttonChoice1.onClick.AddListener(AnswerTrue);
            uiChoice.buttonChoice2.onClick.AddListener(AnswerFalse);
        }
        else if(currentEvent.numberAnswer == 2)
        {
            uiChoice.buttonChoice1.onClick.AddListener(AnswerFalse);
            uiChoice.buttonChoice2.onClick.AddListener(AnswerTrue);
        }
        else
        {
            uiChoice.buttonChoice1.onClick.AddListener(AnswerTrue);
            uiChoice.buttonChoice2.onClick.AddListener(AnswerTrue);
        }
    }

    public void AnswerTrue()
    {
        ResetListenerButtonChoice();
        onTriggerActivated?.Invoke(true);

        actionChoice.Invoke(new EventChoiceMessage(
             currentEvent.answerTrue.money,
             currentEvent.answerTrue.popularity,
             currentEvent.answerTrue.countShelf,
             currentEvent.answerTrue.isDecrementRandomStock,
             currentEvent.answerTrue.isUnlockCat,
             currentEvent.answerTrue.isEventMixUhey));

        if (currentEvent.answerTrue.sfxReward != null)
            MusicManager.instance.PlayNowSFX(currentEvent.answerTrue.sfxReward);

        uiChoice.SetTextKeterangan(currentEvent.answerTrue.stringKeteranganChoice);
    }

    public void AnswerFalse()
    {
        ResetListenerButtonChoice();
        onTriggerActivated?.Invoke(false);

        actionChoice.Invoke(new EventChoiceMessage(
            currentEvent.answerFalse.money,
            currentEvent.answerFalse.popularity,
            currentEvent.answerFalse.countShelf,
            currentEvent.answerFalse.isDecrementRandomStock,
            currentEvent.answerFalse.isUnlockCat,
            currentEvent.answerFalse.isEventMixUhey));

        if (currentEvent.answerFalse.sfxReward != null)
            MusicManager.instance.PlayNowSFX(currentEvent.answerFalse.sfxReward);

        uiChoice.SetTextKeterangan(currentEvent.answerFalse.stringKeteranganChoice);
    }

    public void ResetListenerButtonChoice()
    {
        uiChoice.buttonChoice1.onClick.RemoveAllListeners();
        uiChoice.buttonChoice2.onClick.RemoveAllListeners();
    }

}
