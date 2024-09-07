using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierController : MonoBehaviour
{
    public VFXManager vfxManagerMoney;
    public Transform posVFX;

    public Transform[] queuePositions; // Array untuk menyimpan posisi antrian
    public List<GameObject> npcQueue = new List<GameObject>(); // Antrian NPC


    void Start()
    {
    }

    // Method untuk menambahkan NPC ke antrian
    public void AddNPCToQueue(GameObject npc)
    {
        if (npcQueue.Count < queuePositions.Length)
        {
            npcQueue.Add(npc);
            UpdateQueuePositions();
        }
    }

    // Method untuk memajukan antrian
    public void ProcessNextNPC()
    {
        if (npcQueue.Count > 0)
        {
            GameObject npc = npcQueue[0];

            npcQueue.RemoveAt(0);

            UpdateQueuePositions();
        }
    }


    private void UpdateQueuePositions()
    {

        for (int i = 0; i < npcQueue.Count; i++)
        {
            //npcQueue[i].transform.position = queuePositions[i].position;
            if (i == 0)
            {

                npcQueue[i].GetComponent<NpcController>().SetPosTarget(queuePositions[i], ActionNPC.PAY);
            }
            else
                npcQueue[i].GetComponent<NpcController>().SetPosTarget(queuePositions[i], ActionNPC.QUEUE);
        }
    }

    public void ShowMoneyVFX()
    {
        vfxManagerMoney.GetVFX(posVFX);
    }
}
