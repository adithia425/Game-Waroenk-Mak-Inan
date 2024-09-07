using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMiniGame : MonoBehaviour
{
    [Header("Main")]
    public bool isCorrect;
    public MiniGameManager miniGameManager;
    public float timerDelayStart;

    [Header("Utils")]
    public float rangeVerticalSpawn;
    public List<Sprite> listSpriteLeftHand;
    public List<Sprite> listSpriteRightHand;


    public ButtonMiniGame objectCorrect;
    public RectTransform posisiTangan;
    public Image imageTangan;

    public float moveSpeed; 
    public float proximityThreshold = 10f;

    private bool isMoving = false;


    private bool isStart;
    [SerializeField]
    private float counterStart;


    public RectTransform targetRect;
    /*    private void StartAction()
        {
            StartCoroutine(MoveImageTangan());
        }*/

    private void Update()
    {
        if (isStart)
        {
            counterStart -= Time.deltaTime;
            if (counterStart < 0)
            {
                isStart = false;
                //StartCoroutine(MoveImageTangan());
                isMoving = true;
            }
        }

        if(isMoving)
        {

            if(Vector3.Distance(posisiTangan.anchoredPosition, targetRect.anchoredPosition) > proximityThreshold)
            {
                posisiTangan.anchoredPosition = Vector3.MoveTowards(posisiTangan.anchoredPosition, targetRect.anchoredPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                objectCorrect.HideImage();
                if (isCorrect)
                {
                    miniGameManager.EnemyCorrect();
                }

                isMoving = false;
            }
        }
    }

/*    private IEnumerator MoveImageTangan()
    {
        isMoving = true;
        RectTransform targetRect = objectCorrect.GetComponent<RectTransform>();

        while (Vector3.Distance(posisiTangan.anchoredPosition, targetRect.anchoredPosition) > proximityThreshold)
        {
            posisiTangan.anchoredPosition = Vector3.MoveTowards(posisiTangan.anchoredPosition, targetRect.anchoredPosition, moveSpeed * Time.deltaTime);
            
            yield return null;
        }

        objectCorrect.HideImage();
        if (isCorrect)
        {
            miniGameManager.EnemyCorrect();
        }

        isMoving = false;
    }*/

    public void SetUp(bool isCorrect,bool isLeft, Transform posSpawn, ButtonMiniGame objectCorrect, float timeAction)
    {
        //StopCoroutine(MoveImageTangan());
/*
        float offsetSpawn = Random.Range(-rangeVerticalSpawn, rangeVerticalSpawn);
        transform.position = new Vector3(posSpawn.position.x, offsetSpawn, posSpawn.position.z);*/

        transform.position = posSpawn.position;

        this.isCorrect = isCorrect;
        this.objectCorrect = objectCorrect;

        int indexHand = Random.Range(0, listSpriteLeftHand.Count);
        if(isLeft)
            imageTangan.sprite = listSpriteLeftHand[indexHand];
        else
            imageTangan.sprite = listSpriteRightHand[indexHand];


        /*        RectTransform targetRect = objectCorrect.GetComponent<RectTransform>();*/
        targetRect = objectCorrect.GetComponent<RectTransform>();

        Vector2 direction = (targetRect.anchoredPosition - posisiTangan.anchoredPosition).normalized;

        // Calculate the angle to the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the imageTangan to point towards the target
        posisiTangan.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));



        isStart = true;
        counterStart = timeAction + timerDelayStart;
        //Invoke(nameof(StartAction), timeAction);
    }
}
