using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public List<GameObject> listObjectMainan;
    public List<Sprite> listImageMainan;

    public List<String> listPopularityName;
    public List<int> listKapasitasCount;
    public Sprite GetImageMainan(Mainan mainan)
    {
        int index = Array.IndexOf(Enum.GetValues(mainan.GetType()), mainan);
        return listImageMainan[index];
    }
    public GameObject GetObjectMainan(Mainan mainan)
    {
        int index = Array.IndexOf(Enum.GetValues(mainan.GetType()), mainan);
        return listObjectMainan[index];
    }

    public String GetNamePopularity(int index)
    {
        return listPopularityName[index - 1];
    }

    public int GetCountCapacity(int index)
    {
        return listKapasitasCount[index - 1];
    }

}
