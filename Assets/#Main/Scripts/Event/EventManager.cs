using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    [Header("References")]
    public UIEventManager UIEvent;
    public ChoiceManager choiceManager;


    [Header("Data")]
    public List<ItemEvent> listEventMinor;
    public List<ItemEvent> listEventMajor;



    [Header("Event Detail")]
    public ItemEvent eventActive;
    public bool isEventActive;
    public int counterDialog;

    [Header("Minor")]
    public int indexEventRandom;
    public int triggerTimeEventMinor1;
    public int triggerTimeEventMinor2;


    [Header("Event")]
    public UnityEvent onEventDone;


    [Header("Utils")]
    public float timerHidePanel;

    [Header("Temp")]
    public int dayEventMixUhey;
    public int dayEventKucingMalang;
    public int timeEventMajor;
    int currentDay;
    private void Start()
    {
        currentDay = SaveManager.instance.GetDay();

        SetTriggerTimeEventMinor();


        UIEvent.onDialogDone.AddListener(OnDialogDone);
        
        //Listen ketika selesai menjawab
        choiceManager.onTriggerActivated.AddListener(OnTriggerActivated);
    }

    private void Update()
    {
        CheckTrigger();
    }
    private void CheckTrigger()
    {
        //Atur kapan munculnya
        int time = (int)GameManager.instance.currentTime;
        if (time == triggerTimeEventMinor1)
        {
            if (!isEventActive)
            {
                triggerTimeEventMinor1 = -1;
                StartEventMinor();
            }
        }else if(time == triggerTimeEventMinor2)
        {
            if (!isEventActive)
            {
                triggerTimeEventMinor2 = -1;
                StartEventMinor();
            }
        }


        //Major
        if(currentDay == dayEventMixUhey)
        {
            if (time == timeEventMajor)
            {
                if (!SaveManager.instance.dataInformation.eventMajorMixUhey)
                {
                    //Start Event
                    SaveManager.instance.dataInformation.eventMajorMixUhey = true;
                    StartEventMajor(0);
                }
            }
        }
        else if (currentDay == dayEventKucingMalang)
        {
            if (time == timeEventMajor)
            {
                if (!SaveManager.instance.dataInformation.eventMajorKucingMalang)
                {
                    //Start Event
                    SaveManager.instance.dataInformation.eventMajorKucingMalang = true;
                    StartEventMajor(1);
                }
            }
        }
    }

    private void SetTriggerTimeEventMinor()
    {
        int minTimePagi = 10 * 60;
        int maxTimePagi = 11 * 60;

        triggerTimeEventMinor1 = Random.Range(minTimePagi, maxTimePagi);

        int minTimeSore = 13 * 60;
        int maxTimeSore = 14 * 60;

        triggerTimeEventMinor2 = Random.Range(minTimeSore, maxTimeSore);
    }

    public void StartEventMinor()
    {
        if (SaveManager.instance.GetDay() <= 1) return;

        GameManager.instance.CheckTurnOffFastTime();

        GameManager.instance.StopTime();

        isEventActive = true;

        counterDialog = 0;

        // 0 Kucing
        // 1 Anak malang
        // 2 Anak Bingung
        if (indexEventRandom == -1)
        {
            indexEventRandom = Random.Range(0, 3);
        }
        else if(indexEventRandom == 0)
        {
            //Jika sebelumnya 0 kucing -> malang
            indexEventRandom = 1;
        }else if(indexEventRandom == 1)
        {
            //Jika sebelum 1 malang -> bingung 
            indexEventRandom = 2;
        }
        else
        {
            //Jika sebelum randomn/bingung -> kucing jika udah unlock
            indexEventRandom = 0;

        }



        if (indexEventRandom == 0)
        {
            if (!SaveManager.instance.dataInformation.eventMajorKucingMalang)
            {
                indexEventRandom = 1;
            }
        }
        else if (indexEventRandom == 2) //Jika bingung akan dirandom
        {
            indexEventRandom = Random.Range(2, listEventMinor.Count);
        } 


        eventActive = listEventMinor[indexEventRandom];

        UIEvent.gameObject.SetActive(true);
        SendDetailDialog();
    }

    public void StartEventMajor(int index)
    {
        GameManager.instance.CheckTurnOffFastTime();

        GameManager.instance.StopTime();

        isEventActive = true;

        counterDialog = 0;


        eventActive = listEventMajor[index];

        UIEvent.gameObject.SetActive(true);
        SendDetailDialog();
    }

    private void SendDetailDialog()
    {
        ItemDialog dialog = eventActive.listDialog[counterDialog];
        UIEvent.SetUpDialog(
           dialog.dialog,
           dialog.name,
           dialog.dialogPerson,
           dialog.spriteMakInan,
           dialog.spriteNPC
           );

        if(dialog.sfxDialog != null)
            MusicManager.instance.PlayNowSFX(dialog.sfxDialog);
    }

    public void OnDialogDone()
    {
        counterDialog++;
        int lengthDialog = eventActive.listDialog.Count;
        if (counterDialog < lengthDialog)
        {
            SendDetailDialog();
        }
        else
        {
            //End
            //Debug.Log("Dialog Done");

            //Nyalakan panel choice
            choiceManager.ActivatedChoice();
            choiceManager.SetEvent(eventActive);


            //eventActive = null;
            //isEventActive = false;
            //onEventDone?.Invoke();
        }
    }

    public void OnTriggerActivated(bool answer)
    {
        //Aktifkan effect event
        if (answer)
        {
            //Trigger bagus
            //Debug.Log("Trigger Jawaban Benar");
 
        }
        else
        {
            //Trigger jelek
            //Debug.Log("Trigger Jawaban Salah");

        }

        GameManager.instance.PlayTime();
        isEventActive = false;

        Invoke(nameof(ResetPanelUI), timerHidePanel);
    }

    private void ResetPanelUI()
    {
        choiceManager.uiChoice.gameObject.SetActive(false);
        UIEvent.gameObject.SetActive(false);
    }
}

public enum DialogPerson
{
    NARATOR,
    MAK_INAN,
    NPC,
    CHOICE
}