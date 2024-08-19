using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CanvasManager canvas;
    public DatabaseManager database;
    public CashierController cashierController;
    public ThiefManager thiefManager;

    [SerializeField]
    private int currentMoney;
    private int currentPopularity;

    public int counterTargetPopularity;


    //public int levelUnlockMainan;

    private Mainan[] mainanArray;

    [Header("Pop Up")]
    public GameObject panelPopUp;
    public TextMeshProUGUI textDescPanelPop;
    public PanelThief panelThief;

    [Header("Utils")]
    public bool isFastForward;
    public Image imageFastForward;
    public Sprite spriteNormal;
    public Sprite spriteFast;

    [Header("Testing")]
    public bool isTestingTheif;

    [Header("Time")]
    public float currentTime; // Waktu dalam detik sejak jam 08:00
    private const string TimePrefKey = "GameTime";

    private void Awake()
    {
        instance = this;

        mainanArray = (Mainan[])System.Enum.GetValues(typeof(Mainan));
    }

    private void Start()
    {
        //Load Data
        currentMoney = SaveManager.instance.dataInformation.money;
        currentPopularity = SaveManager.instance.dataInformation.popularity;


        int lvPop = SaveManager.instance.GetLevelPopularity();
        canvas.SetTextNamePopularity(lvPop);

        if (PlayerPrefs.HasKey(TimePrefKey))
        {
            currentTime = PlayerPrefs.GetFloat(TimePrefKey);
        }
        else
        {
            currentTime = 8 * 60 * 60; // Mulai dari jam 08:00 (8 jam dalam detik)
        }

        UpdateTimeDisplay();
    }

    public int GetCurrentMoney()
    {
        return currentMoney;
    }
    public int GetCurrentPopularity()
    {
        return currentPopularity;
    }

    public int GetTargetPopularity()
    {
        int lvPop = SaveManager.instance.GetLevelPopularity();
        return (lvPop * counterTargetPopularity);
    }
    public void AddMoney(int val)
    {
        currentMoney += val;
        SaveManager.instance.SaveGame();
    }
    public void AddPopularity(int val)
    {
        currentPopularity += val;

        if (currentPopularity < 0) currentPopularity = 0;

        int lvPop = SaveManager.instance.GetLevelPopularity();
        if (currentPopularity >= GetTargetPopularity())
        {
            currentPopularity = 0;
            lvPop++;
            canvas.SetTextNamePopularity(lvPop);
            SaveManager.instance.SetLevelPopularity(lvPop);
            MusicManager.instance.PlaySFX(SFX.LEVELUPPOP);
        }

        SaveManager.instance.SaveGame();
    }
   

    public int GetIndexMainan(Mainan mainan)
    {
        return System.Array.IndexOf(mainanArray, mainan);
    }

    void Update()
    {
        canvas.SetTextMoney(currentMoney);
        canvas.SetTextPopularity(currentPopularity, GetTargetPopularity());

        currentTime += Time.deltaTime;

        // Reset waktu jika sudah melewati 24 jam
        if (currentTime >= 24 * 60 * 60)
        {
            currentTime -= 24 * 60 * 60;
        }

        UpdateTimeDisplay();
    }


    public void ShowPanelPopUp(string desc)
    {
        panelPopUp.SetActive(true);
        textDescPanelPop.text = desc;
        MusicManager.instance.PlaySFX(SFX.POPUPNOTIF);
    }

    public void ShowPanelThief(GameObject npc)
    {
        panelThief.gameObject.SetActive(true);
        panelThief.npc = npc;
    }



    public void TestingThief(bool con)
    {
        isTestingTheif = con;
    }

    public void ChangeFastForward()
    {
        isFastForward = !isFastForward;

        if(isFastForward)
        {
            Time.timeScale = 2;
            imageFastForward.sprite = spriteFast;
        }
        else
        {
            Time.timeScale = 1;
            imageFastForward.sprite = spriteNormal;
        }
    }


    //Time
    void UpdateTimeDisplay()
    {
        int hours = Mathf.FloorToInt(currentTime / 3600);
        int minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        canvas.SetTextTime(minutes.ToString("00") + ":" + seconds.ToString("00"));
    }

    public void SaveTime()
    {
        PlayerPrefs.SetFloat(TimePrefKey, currentTime);
        PlayerPrefs.Save();
    }
}
