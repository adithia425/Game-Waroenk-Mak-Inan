using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public GameObject objectCat;

    public GameObject effectSpawn;
    void Start()
    {
        CheckActivatedCat();
    }

    public void CheckActivatedCat()
    {
        if (SaveManager.instance.dataInformation.eventMajorKucingMalang)
        {
            objectCat.SetActive(true);
            Instantiate(effectSpawn, objectCat.transform.position, Quaternion.identity) ;
        }
    }
}
