using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public CanvasMiniGame canvas;
    public TargetUIMiniGame targetUI;

    private List<Mainan> listMainan;
    public Sprite[] listSprites; // Array of sprites
    public ButtonMiniGame[] listButtonMiniGame; // Array of existing UI Image objects

    [Header("Main")]
    public bool isTesting;
    public int currentLevelMainan;
    public bool isPlaying;
    public float timeLimit = 3.0f; // Time limit for player to click
    public int counterIndexTarget = 0;
    public bool playerCanClick;
    public bool gameEnd;
    private int targetIndexPosisiImage;
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

    void Start()
    {
        //Get dari playerpref

        levelUnlockMainan = SaveManager.instance.listDataShelf.levelShelf;

        canvas.PanelTutorial(true);

        listMainan = new List<Mainan>();

        foreach (Mainan mainan in System.Enum.GetValues(typeof(Mainan)))
        {
            listMainan.Add(mainan);
        }


        RandomTargetList();
        RandomMainan();
    }

    public void ReadyGame()
    {
        isReadyGame = true;
        counterTime = 1;
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

        yield return new WaitForSeconds(1f);

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

    private void RandomTargetList()
    {
        listTargetMainan = new List<Mainan>();

        List<Mainan> availableMainan = listMainan.GetRange(0, Mathf.Min(levelUnlockMainan, listMainan.Count));

        for (int i = 0; i < 20; i++)
        {
            int randomIndex = Random.Range(0, availableMainan.Count);
            listTargetMainan.Add(availableMainan[randomIndex]);
            listTargetImage[i].sprite = listSprites[randomIndex];
        }
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
            gameEnd = true;
            canvas.PanelTimer(false);
            canvas.PanelLose(true);
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


        targetIndexPosisiImage = Random.Range(0, listButtonMiniGame.Length);

        targetMainan = listTargetMainan[counterIndexTarget];

        //Ambil index dari mainan
        Mainan[] mainanArray = (Mainan[])System.Enum.GetValues(typeof(Mainan));
        int indexTargetMainan = System.Array.IndexOf(mainanArray, targetMainan);

        listButtonMiniGame[targetIndexPosisiImage].SetMainan(targetMainan, listSprites[indexTargetMainan]);

        for (int i = 0; i < listButtonMiniGame.Length; i++)
        {
            if (i != targetIndexPosisiImage)
            {
                int randomIndex = Random.Range(0, listSprites.Length);
                if (randomIndex == indexTargetMainan)
                {
                    if (randomIndex == 0)
                        randomIndex++;
                    else
                        randomIndex--;
                }
                listButtonMiniGame[i].SetMainan(listMainan[randomIndex], listSprites[randomIndex]);
            }
        }

        if(!gameEnd)
            SetEnemy();
    }

    public void PlayerClicked()
    {
        playerCanClick = false;
    }
    public void PlayerCorrect()
    {
        if (!isPlaying) return;
        isPlaying = false;

        counterIndexTarget++;
        targetUI.MoveContent();


        //listTargetMainan.Count
        if(isTesting)
        {
            GameWinDone();
            return;
        }
        else
        {
            if (counterIndexTarget >= listTargetMainan.Count)
            {
                GameWinDone();
                return;
            }
        }


        RandomMainan();
    }


    private void GameWinDone()
    {
        Debug.Log("Game Win");
        canvas.PanelTimer(false);
        canvas.PanelWin(true);
        gameEnd = true;

        //Unlock Mainan
        int levelShelf = SaveManager.instance.listDataShelf.levelShelf;
        int costUpgrade = SaveManager.instance.listDataShelf.dataShelf[levelShelf].priceToUnlock;
        SaveManager.instance.AddMoney(-costUpgrade);
        SaveManager.instance.LevelUpShelf();
        MusicManager.instance.PlaySFX(SFX.WINMINIGAME);
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
        panelSwipe.GetComponent<Animator>().SetTrigger("Play");

        //Invoke(nameof(HidePanelSwipe), 1.1f);
    }

    private void HidePanelSwipe()
    {
        panelSwipe.SetActive(false);
    }
}
