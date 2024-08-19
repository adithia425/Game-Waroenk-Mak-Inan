using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform posTarget;

    public Mainan choosedMainan;
    public ActionNPC actionNPC;

    [Header("Main")]
    public bool testingThief;
    public bool isThief;
    public bool isBoy;
    public GameObject modelBoy;
    public GameObject modelGirl;

    [Header("Icon Bar")]
    public GameObject panelIcon;
    public Image imageIcon;
    public Sprite spriteAngry;
    public Sprite spriteHappy;
    public Sprite spriteThief;
    //public Sprite spriteSandal;

    [Header("Utils")]
    public GameObject objPegang;
    public Transform posPegang;
    public float rangeDistanceEtalase;
    public float rangeDistanceCashier;
    public float rangeDistanceOut;

    public bool isPaying;


    public float timerCollectMainan;
    public float timerPayCashier;

    public bool isCheckBug;
    public float timerAntiBug;
    [SerializeField]
    private float counterTimerAntiBug;

    RaycastHit hit;

    [Header("Component")]
    public NpcAnimation animBoy;
    public NpcAnimation animGirl;



/*    [Header("Event")]
    public UnityEvent eventClickThief;*/
    void Start()
    {
        
    }

    public void SetNPC()
    {
        //Pilih Gender
        isBoy = Random.Range(0f, 1f) < 0.5f ?  true : false;


        isThief = Random.Range(0f, 1f) < 0.1f ? true : false;

        if (GameManager.instance.isTestingTheif) isThief = true;

        if (isBoy)
        {
            modelBoy.SetActive(true);
            modelGirl.SetActive(false);
        }
        else
        {
            modelBoy.SetActive(false);
            modelGirl.SetActive(true);
        }

        //Pilih mainan
        choosedMainan = GetRandomMainan();
        Destroy(objPegang);

        imageIcon.sprite = GameManager.instance.database.GetImageMainan(choosedMainan);

        posTarget = ShelfManager.instance.GetPosNPCShelf(choosedMainan);
        agent.SetDestination(posTarget.position);

        isCheckBug = true;
        counterTimerAntiBug = timerAntiBug;

        isPaying = false;
    }

    Mainan GetRandomMainan()
    {
        List<Mainan> listMainanUnlock = ShelfManager.instance.GetListUnlockedMainan();
        int index = Random.Range(0, listMainanUnlock.Count);
        return listMainanUnlock[index];

/*        System.Array values = System.Enum.GetValues(typeof(Mainan));
        System.Random random = new System.Random();
        Mainan randomMainan = (Mainan)values.GetValue(random.Next(values.Length));
        return randomMainan;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (posTarget != null && !agent.pathPending)
        {
            if (isBoy)
                animBoy.SetWalking(true);
            else
                animGirl.SetWalking(true);

            switch (actionNPC)
            {
                case ActionNPC.SPAWN:
                    if(agent.remainingDistance <= rangeDistanceEtalase)
                    {

                        posTarget = null;
                        CheckAction();
                    }
                    break;
                case ActionNPC.PAY:
                    if (agent.remainingDistance <= rangeDistanceCashier)
                    {

                        posTarget = null;
                        CheckAction();
                    }
                    break;
                case ActionNPC.QUEUE:
                    if (agent.remainingDistance <= rangeDistanceCashier)
                    {

                        posTarget = null;
                        CheckAction();
                    }
                    break;
                case ActionNPC.OUT:
                    if (agent.remainingDistance <= rangeDistanceOut)
                    {

                        posTarget = null;
                        CheckAction();
                    }
                    break;
            }
        }
        else
        {
            if (isBoy)
                animBoy.SetWalking(false);
            else
                animGirl.SetWalking(false);
        }





        //Tambahan
        panelIcon.transform.forward = Camera.main.transform.forward;

        if (isCheckBug)
        {
            counterTimerAntiBug -= Time.deltaTime;
            if (counterTimerAntiBug <= 0)
            {
                //Suruh pulang
                isCheckBug = false;
                ActionAngry();
            }
        }


/*        if (Input.GetMouseButtonDown(0))
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(camRay, out hit))
            {
                Debug.Log(hit.transform.name);
                //posTarget = null;
                //Show panel thief
                //GameManager.instance.ShowPanelThief(this);
                
            }
        }*/
    }


    public void CheckAction()
    {

        counterTimerAntiBug = timerAntiBug;

        switch (actionNPC)
        {
            case ActionNPC.SPAWN:
                //Animasi dan delay
                Invoke(nameof(ActionSpawn), timerCollectMainan);
                break;
            case ActionNPC.QUEUE:
                //Cek jika urutan pertama
                break;
            case ActionNPC.PAY:
                Invoke(nameof(ActionPay), timerPayCashier);
                break;
            case ActionNPC.OUT:
                ActionOut();
                break;
            default:
                break;
        }
    }

    private void ActionSpawn()
    {
        //Check Stok
        ShelfController shelf = ShelfManager.instance.GetShelfController(choosedMainan);
        bool getMainan = shelf.DecrementStok();

        if (getMainan)
        {
            //Masuk Antrian
            if (isThief)
            {
                ActionThief();
            }
            else
            {
                GameManager.instance.cashierController.AddNPCToQueue(gameObject);
            }
            objPegang = Instantiate(GameManager.instance.database.GetObjectMainan(choosedMainan), posPegang.position, posPegang.rotation, posPegang);
        }
        else
        {
            //Jika Tidak
            ActionAngry();
        }
    }

    public void SetPosTarget(Transform pos, ActionNPC action)
    {
        if (isPaying) return;

        actionNPC = action;
        if(actionNPC == ActionNPC.PAY)
        {
            isPaying = true;
        }

        posTarget = pos;
        agent.SetDestination(posTarget.position);
    }

    public void StopThief()
    {
        if (!isThief) return;

        counterTimerAntiBug = 120;
        agent.isStopped = true;
        posTarget = null;
        GameManager.instance.ShowPanelThief(gameObject);
    }

    public void ActionThief()
    {
        imageIcon.sprite = spriteThief;
        posTarget = NpcManager.instance.posOut;
        agent.SetDestination(posTarget.position);
        actionNPC = ActionNPC.OUT;
    }
    public void ActionThiefSuccess()
    {
        //NPC Kabur
        agent.isStopped = false;
        GameManager.instance.AddPopularity(-3);
        posTarget = NpcManager.instance.posOut;
        agent.SetDestination(posTarget.position);
        actionNPC = ActionNPC.OUT;
    }

    public void ActionThiefFailed()
    {
        GameManager.instance.thiefManager.ShootSendal(gameObject);
    }
    public void ActionThieFailedDone()
    {
        agent.isStopped = false;
        GameManager.instance.AddPopularity(5);
        actionNPC = ActionNPC.OUT;
        MusicManager.instance.PlaySFX(SFX.NPCHILANG);
        gameObject.SetActive(false);
    }
    public void ActionPay()
    {
        GameManager.instance.AddPopularity(1);
        FinishPaying();

        imageIcon.sprite = spriteHappy;
        GameManager.instance.AddMoney(ShelfManager.instance.GetPriceShelf(choosedMainan));
        posTarget = NpcManager.instance.posOut;
        agent.SetDestination(posTarget.position);
        actionNPC = ActionNPC.OUT;
    }

    private void ActionOut()
    {
        actionNPC = ActionNPC.SPAWN;
        gameObject.SetActive(false);
    }

    private void ActionAngry()
    {
        GameManager.instance.AddPopularity(-2);
        imageIcon.sprite = spriteAngry;
        posTarget = NpcManager.instance.posOut;
        agent.SetDestination(posTarget.position);
        actionNPC = ActionNPC.OUT;
    }



    //Selesai Antrian
    public void FinishPaying()
    {
        GameManager.instance.cashierController.ProcessNextNPC();
    }



/*    private void OnMouseEnter()
    {
        if(isThief)
        {
            imageIcon.sprite = spriteSandal;
        }
    }

    private void OnMouseExit()
    {
        if (isThief)
        {
            imageIcon.sprite = spriteThief;
        }
    }

    private void OnMouseDown()
    {
        if (isThief)
        {
            eventClickThief?.Invoke();
        }
    }*/

}

public enum ActionNPC
{
    SPAWN,
    PAY,
    QUEUE,
    OUT
}