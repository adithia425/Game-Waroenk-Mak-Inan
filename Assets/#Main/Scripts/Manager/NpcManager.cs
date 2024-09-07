using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager instance;

    public Transform posSpawn;
    public Transform posOut;

    public List<NpcController> listNpc;

    [Header("Variable")]
    public float minTimerSpawn;
    public float maxTimerSpawn;
    public float counterTimerSpawn;
    public int maxNPCSpawn;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetSpawnNPC();
    }


    public void SetSpawnNPC()
    {
        counterTimerSpawn = Random.Range(minTimerSpawn, maxTimerSpawn);

        Invoke(nameof(SpawnNPC), counterTimerSpawn);
    }


    public void SpawnNPC()
    {
        if (!GameManager.instance.isTimePlay)
        {
            SetSpawnNPC();
            return;
        }

        List<NpcController> inactiveNPCs = new List<NpcController>();
        NpcController choosedNPC;

        foreach (NpcController npc in listNpc)
        {
            if (!npc.gameObject.activeSelf)
            {
                inactiveNPCs.Add(npc);
            }
        }

        //Ambil data level popularitas
        int levelCap = SaveManager.instance.dataInformation.levelCapacity;
        maxNPCSpawn = DatabaseManager.instance.GetCountCapacity(levelCap);

        if (listNpc.Count - inactiveNPCs.Count >= maxNPCSpawn)
        {
            SetSpawnNPC();
            return;
        }

        if (inactiveNPCs.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveNPCs.Count);
            choosedNPC = inactiveNPCs[randomIndex];
        }
        else
        {
            SetSpawnNPC();
            return;
        }

        choosedNPC.gameObject.SetActive(true);
        choosedNPC.transform.position = posSpawn.position;
        choosedNPC.SetNPC();

        SetSpawnNPC();
    }



    public void ForceNPCToQuit()
    {
        for (int i = 0; i < listNpc.Count; i++)
        {
            if(listNpc[i].gameObject.activeSelf)
            {
                listNpc[i].ForceToQuit();
            }
        }
    }
}
