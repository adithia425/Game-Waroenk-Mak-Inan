using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("References")]
    public CanvasManager canvas;
    public CameraController cameraController;
    public CashierController cashierController;
    public ThiefManager thiefManager;
    public CatManager catManager;


    [Header("Data Main")]
    [SerializeField]
    private int currentMoney;
    private int currentPopularity;
    public int counterTargetPopularity;


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
    public bool isTimePlay;
    public bool isClosedShop;
    [SerializeField] private int startTime = 8 * 60;
    [SerializeField] private int endTime = 17 * 60;
    public float currentTime;
    public int timeScaleFactor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Load Data
        currentMoney = SaveManager.instance.dataInformation.money;
        currentPopularity = SaveManager.instance.dataInformation.popularity;


        int lvPop = SaveManager.instance.GetLevelPopularity();
        canvas.SetTextNamePopularity(lvPop);


        PrepareScene();
        UpdateTimeDisplay();
    }

    private void PrepareScene()
    {
        currentTime = startTime;
        canvas.SetTextDay(SaveManager.instance.GetDay().ToString());


        //Apakah selesai berbelanja
        if (PlayerPrefs.GetInt("PlayMiniGame") == 1)
        {
            PlayerPrefs.SetInt("PlayMiniGame", 0);


            MusicManager.instance.PlaySFX(SFX.OPENSHOP);

            ShelfManager.instance.SetShelf();

            //Get Data barang yang berhasil diambil, tambahkan ke etalase
            List<int> listStockCollect = DatabaseManager.instance.listCounterMainanCollected;
            ShelfManager.instance.SetUpStockShelf(listStockCollect);

            canvas.PanelPrepare(false);
            isClosedShop = false;
            isTimePlay = true;
            return;
        }


        ShelfManager.instance.SetShelf();
        //Jika belum berbelanja
        canvas.PanelPrepare(true);
        isClosedShop = true;
        isTimePlay = false;
    }
    public void OpenShop()
    {
        MusicManager.instance.PlaySFX(SFX.OPENSHOP);
        ShelfManager.instance.SetShelf();
        canvas.PanelPrepare(false);
        isTimePlay = true;
        isClosedShop = false;

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
   

/*    public int GetIndexMainan(Mainan mainan)
    {
        return System.Array.IndexOf(mainanArray, mainan);
    }*/

    void Update()
    {
        canvas.SetTextMoney(currentMoney);
        canvas.SetTextPopularity(currentPopularity, GetTargetPopularity());

        if (isTimePlay)
        {
            currentTime += Time.deltaTime * timeScaleFactor;

            if (currentTime >= endTime)
            {
                currentTime = endTime;
                isTimePlay = false;
                ClosedShop();
            }

        }
        UpdateTimeDisplay();
    }

    public void StopTime()
    {
        isTimePlay = false;
    }
    public void PlayTime()
    {
        if (isClosedShop) return;
        isTimePlay = true;
    }

    public void ClosedShop()
    {
        CheckTurnOffFastTime();


        isClosedShop = true;
        isTimePlay = false;

        MusicManager.instance.PlaySFX(SFX.CLOSESHOP);
        NpcManager.instance.ForceNPCToQuit();
        SaveManager.instance.ResetDay();

        //panel recap active dan init

        //canvas.SetTextClosedShop("Klik untuk berganti hari");
        canvas.PanelRecap(true);
    }

    public void ChangeDay()
    {
        SaveManager.instance.AddDay();
        ScenesManager.instance.ChangeScene("GamePlay");
    }


    private void ClosedToOpen()
    {
        PrepareScene();
        UpdateTimeDisplay();
        canvas.PanelLoading(false);
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

    public void CheckTurnOffFastTime()
    {
        if (isFastForward)
            ChangeFastForward();
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
        int hours = Mathf.FloorToInt(currentTime / 60);
        int minutes = Mathf.FloorToInt((currentTime % 60));
        canvas.SetTextTime(hours.ToString("00") + ":" + minutes.ToString("00"));
    }

}
