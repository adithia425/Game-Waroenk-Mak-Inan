using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public CanvasMiniGame canvas;
    public TargetUIMiniGame targetUI;

    [SerializeField]
    private List<Mainan> listMainan;
    [SerializeField]
    private List<Mainan> availableMainan;
    public Sprite[] listBoxNoOutline;
    public Sprite[] listBoxWithOutline; // Array of sprites
    public ButtonMiniGame[] listButtonMiniGame; // Array of existing UI Image objects

    [Header("Main")]
    public int countMainanChoosed;
    public int currentLevelMainan;
    public bool isPlaying;
    public float timeLimit = 3.0f; // Time limit for player to click
    public int counterIndexTarget = 0;
    public bool playerCanClick;
    public bool gameEnd;
    [SerializeField]
    private int targetIndexPosisiImage; //Index target mainan
    [SerializeField]
    private int indexMainanSpecials; // Index mainan emas
    private float timer;

    public Mainan targetMainan;
    public List<Mainan> listTargetMainan;
    public List<Image> listTargetImage;


    public int levelUnlockMainan;

    [Header("Enemy")]
    public List<EnemyMiniGame> listEnemy;
    public Transform posLeft;
    public Transform posRight;


    [Header("UI")]
    public GameObject panelSwipe;

    [Header("Ready")]
    public Image imageReady;
    public Sprite[] listSpriteReady;
    public bool isReadyGame;
    public int counterTime;

    [Header("Data")]
    public int[] listDurasiGame;
    public float[] listKecepatanTangan;

    public float chanceMainanSpecials;



    [Header("VFX")]
    public GameObject vfxGameEnd;

    void Start()
    {
        canvas.PanelTutorial(true);

        //Reset Database mainan collected
        DatabaseManager.instance.ResetListMainanCollected();

        //Get dari playerpref
        levelUnlockMainan = SaveManager.instance.listDataShelf.levelShelf;


        //Isi data list Mainan
        listMainan = new List<Mainan>();

        foreach (Mainan mainan in System.Enum.GetValues(typeof(Mainan)))
        {
            listMainan.Add(mainan);
        }

        availableMainan = listMainan.GetRange(0, Mathf.Min(levelUnlockMainan, listMainan.Count));

        RandomTargetList();
        RandomMainan();
    }

    public void ReadyGame()
    {
        isReadyGame = true;
        counterTime = 1;


        MusicManager.instance.PlaySFX(SFX.MG321);

        imageReady.gameObject.SetActive(true);
        canvas.PanelTutorial(false);

        StartCoroutine(CountReady());
    }

    IEnumerator CountReady()
    {
        switch (counterTime)
        {
            case 1:
                imageReady.sprite = listSpriteReady[counterTime - 1];
                break;
            case 2:
                imageReady.sprite = listSpriteReady[counterTime - 1];
                break;
            case 3:
                imageReady.sprite = listSpriteReady[counterTime - 1];
                break;
            case 4:
                imageReady.sprite = listSpriteReady[counterTime - 1];
                break;
            case 5:
                isReadyGame = false;
                imageReady.gameObject.SetActive(false);
                StartGame();
                break;
        }

        yield return new WaitForSeconds(1.1f);

        if (isReadyGame)
        { 
            counterTime++;
            StartCoroutine(CountReady());
        }

    }

    public void StartGame()
    {
        timeLimit = listDurasiGame[levelUnlockMainan];
        timer = timeLimit;
        gameEnd = false;

        playerCanClick = true;
        canvas.PanelTimer(true);
        canvas.SetTimer(timeLimit);
        panelSwipe.SetActive(true);

        RandomMainan();
    }


    private void SetEnemy()
    {
        int indexEnemyCorrect = Random.Range(0, listEnemy.Count);

        for (int i = 0; i < listEnemy.Count; i++)
        {
            float probLeft = Random.Range(0f, 10f);
            Transform posSpawn;
            bool isLeft;
            if(probLeft >= 5f)
            {
                isLeft = true;
                posSpawn = posLeft;
            }
            else
            {
                isLeft = false;
                posSpawn = posRight;
            }

            float targetKecepatanTangan = listKecepatanTangan[levelUnlockMainan];
            if (i == indexEnemyCorrect)
            {
                //Set Tujuan Benar
                //Atur Waktu 
                listEnemy[i].SetUp(true, isLeft, posSpawn, listButtonMiniGame[targetIndexPosisiImage], targetKecepatanTangan);
            }
            else
            {
                //Set Mainan Random
                float timeAction = Random.Range(0.25f, targetKecepatanTangan);
                int randomIndexPosisiImage = Random.Range(0, listButtonMiniGame.Length);

                if(randomIndexPosisiImage == targetIndexPosisiImage)                                    //Agar mulai setelah tangan utama
                    listEnemy[i].SetUp(true, isLeft, posSpawn, listButtonMiniGame[randomIndexPosisiImage], (timeAction/2) + targetKecepatanTangan); 
                else
                    listEnemy[i].SetUp(false, isLeft, posSpawn, listButtonMiniGame[randomIndexPosisiImage], timeAction);
            }
        }
    }

    void Update()
    {

        if (gameEnd) return;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            canvas.UpdateTimer(timer);
        }
        else
        {
            TimeEnd();
        }
    }

    public void RandomMainan()
    {
        StopCoroutine(StartRandomMainan());
        StartCoroutine(StartRandomMainan());
    }

    IEnumerator StartRandomMainan()
    {
        Swipe();
        yield return new WaitForSeconds(.5f);

        isPlaying = true;

        if(!isReadyGame) playerCanClick = true;



        targetMainan = listTargetMainan[counterIndexTarget];

        //Ambil index dari mainan
        Mainan[] mainanArray = (Mainan[])System.Enum.GetValues(typeof(Mainan));
        int indexTargetMainan = System.Array.IndexOf(mainanArray, targetMainan);

        //Random posisi mainan target
        targetIndexPosisiImage = Random.Range(0, listButtonMiniGame.Length);

        listButtonMiniGame[targetIndexPosisiImage].SetMainan(targetMainan, listBoxNoOutline[indexTargetMainan], false);


        //Random apakah akan muncul mainan specials
        bool isActiveSpecials = Random.Range(0, 100) < chanceMainanSpecials ? true : false;

        if (gameEnd) isActiveSpecials = false;

        int indexPosisiSpecial = - 1;
        if (isActiveSpecials)
        {
            //random indexSpecial
            indexPosisiSpecial = Random.Range(0, listButtonMiniGame.Length);
            if (indexMainanSpecials == targetIndexPosisiImage) indexMainanSpecials--;

            //List mainan yg ke unlock
            // 9 karena ada 9 mainan biasa
            int randomIndexSpecial = DatabaseManager.instance.countMainanInGame + Random.Range(0, availableMainan.Count);
            Debug.Log($"Index {indexPosisiSpecial} Spesial {listMainan[randomIndexSpecial]}");

            listButtonMiniGame[indexPosisiSpecial].SetMainan(listMainan[randomIndexSpecial], listBoxNoOutline[randomIndexSpecial], true);
        }

        for (int i = 0; i < listButtonMiniGame.Length; i++)
        {
            if (i != targetIndexPosisiImage && i != indexPosisiSpecial)
            {
                
                int randomIndex = Random.Range(0, listBoxNoOutline.Length / 2);
                if (randomIndex == indexTargetMainan)
                {
                    if (randomIndex == 0)
                        randomIndex++;
                    else
                        randomIndex--;
                }

                listButtonMiniGame[i].SetMainan(listMainan[randomIndex], listBoxNoOutline[randomIndex], false);
                
            }
        }

        if(!gameEnd)
            SetEnemy();
    }

    public void SetPlayerCantClicked()
    {
        playerCanClick = false;
    }
    public void PlayerCorrect()
    {
        if (!isPlaying) return;
        isPlaying = false;

        MusicManager.instance.PlaySFX(SFX.SFXMGCORRECT);

        int indexTargetMainan = DatabaseManager.instance.GetIndexMainan(targetMainan);
        DatabaseManager.instance.AddListMainanCollected(indexTargetMainan, DatabaseManager.instance.counterCollectedStockMainan);

        counterIndexTarget++;
        targetUI.MoveContent();

        if (counterIndexTarget >= countMainanChoosed)
        {
            GameDone();
            return;
        }
        


        RandomMainan();
    }

    private void TimeEnd()
    {
        canvas.PanelTimeEnd(true);

        TriggerEnding();
    }
    private void GameDone()
    {
        canvas.PanelDone(true);

        TriggerEnding();
        //Unlock Mainan
        /*        int levelShelf = SaveManager.instance.listDataShelf.levelShelf;
                int costUpgrade = SaveManager.instance.listDataShelf.dataShelf[levelShelf].priceToUnlock;
                SaveManager.instance.AddMoney(-costUpgrade);
                SaveManager.instance.LevelUpShelf();*/

    }

    public void TriggerEnding()
    {
        MusicManager.instance.PlaySFX(SFX.DONEMINIGAME);

        canvas.PanelTimer(false);
        vfxGameEnd.SetActive(true);

        gameEnd = true;
        PlayerPrefs.SetInt("PlayMiniGame", 1);

        Invoke(nameof(BackToGamePlay), 3f);
    }


    private void BackToGamePlay()
    {
        ScenesManager.instance.ChangeScene("GamePlay");
    }
    public void EnemyCorrect()
    {
        if (!isPlaying) return;
        Debug.Log("Enemy Correct");
        isPlaying = false;

        RandomMainan();
    }

    public void Swipe()
    {
        if (panelSwipe.activeInHierarchy)
        {
            MusicManager.instance.PlaySFX(SFX.MGSWIPE);
            panelSwipe.GetComponent<Animator>().SetTrigger("Play");
        }
        //Invoke(nameof(HidePanelSwipe), 1.1f);
    }

    private void HidePanelSwipe()
    {
        panelSwipe.SetActive(false);
    }


    #region Setup Target Mainan

    //Set Target List Mainan
    private void RandomTargetList()
    {
        listTargetMainan = new List<Mainan>();
/*
        List<Mainan> availableMainan = listMainan.GetRange(0, Mathf.Min(levelUnlockMainan, listMainan.Count));

        for (int i = 0; i < 20; i++)
        {
            int randomIndex = Random.Range(0, availableMainan.Count);
            listTargetMainan.Add(availableMainan[randomIndex]);
            listTargetImage[i].sprite = listBoxWithOutline[randomIndex];
        }
*/

        countMainanChoosed = DatabaseManager.instance.countMainanChoosed;
        List<int> choosedListMainan = DatabaseManager.instance.listCounterMainanMiniGame;


        List<int> listSortInteger = ConvertToListTarget(choosedListMainan);

        List<int> listRandomInteger = Shuffle(listSortInteger);

        listTargetMainan = ConvertIntListToEnumList(listRandomInteger);

        for (int i = 0; i < countMainanChoosed; i++)
        {
            listTargetImage[i].sprite = listBoxWithOutline[listRandomInteger[i]];
        }
    }

    List<int> ConvertToListTarget(List<int> originalList)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < originalList.Count; i++)
        {
            for (int j = 0; j < originalList[i]; j++)
            {
                result.Add(i);
            }
        }

        return result;
    }

    List<int> Shuffle(List<int> list)
    {
        List<int> shuffledList = new List<int>(list);

        // Mengacak elemen-elemen di dalam list
        for (int i = 0; i < shuffledList.Count; i++)
        {
            int randomIndex = Random.Range(0, shuffledList.Count);
            int temp = shuffledList[i];
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }

        return shuffledList;
    }

    List<Mainan> ConvertIntListToEnumList(List<int> listInt)
    {
        List<Mainan> listEnum = new List<Mainan>();

        foreach (int index in listInt)
        {
            // Mengonversi integer ke enum dan menambahkannya ke list enum
            listEnum.Add((Mainan)index);
        }

        return listEnum;
    }


    #endregion
}
