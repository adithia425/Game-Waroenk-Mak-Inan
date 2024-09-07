using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public List<Transform> listPosisiIdle;
    public float idleTime; // Waktu idle dalam detik
    //public Animator animator; // Untuk mengontrol animasi
    private NavMeshAgent navMeshAgent;
    private StateAction currentState;

    public Animator animCat;


    public float minTimerMeong;
    public float maxTimerMeong;
    private float _timerMeong;

    [Header("SFX")]
    public List<AudioClip> listSfxMeong;

    void Start()
    {
        _timerMeong = Random.Range(minTimerMeong, maxTimerMeong);
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomState();
    }

    void Update()
    {
        if (currentState == StateAction.WALK)
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                SetRandomState();
            }
        }

        _timerMeong -= Time.deltaTime;
        if(_timerMeong <= 0)
        {
            _timerMeong = Random.Range(minTimerMeong, maxTimerMeong);
            MusicManager.instance.PlayNowSFX(listSfxMeong[Random.Range(0,listSfxMeong.Count)]);
        }
    }

    void SetRandomState()
    {
        currentState = (StateAction)Random.Range(0, System.Enum.GetValues(typeof(StateAction)).Length);

        switch (currentState)
        {
            case StateAction.WALK:
                MoveToRandomPosition();
                break;

            case StateAction.IDLE:
                Stand();
                break;

            case StateAction.SIT:
                Sit();
                break;
        }
    }

    void MoveToRandomPosition()
    {
        Transform targetPos = listPosisiIdle[Random.Range(0, listPosisiIdle.Count)];
        navMeshAgent.SetDestination(targetPos.position);

        animCat.SetBool("stand", true);
        animCat.SetBool("walk", true);
        animCat.SetBool("idle", false);
    }

    IEnumerator Idle()
    {
        yield return new WaitForSeconds(idleTime);

        SetRandomState();
    }

    void Sit()
    {
        animCat.SetBool("stand", false);
        animCat.SetBool("walk", false);
        animCat.SetBool("idle", true);
        StartCoroutine(Idle());
    }

    void Stand()
    {
        animCat.SetBool("stand", true);
        animCat.SetBool("walk", false);
        animCat.SetBool("idle", true);
        StartCoroutine(Idle());
    }
}

public enum StateAction
{
    WALK,
    IDLE,
    SIT
}
