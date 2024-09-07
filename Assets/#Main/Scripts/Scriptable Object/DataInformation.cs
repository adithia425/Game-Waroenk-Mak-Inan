using UnityEngine;

[CreateAssetMenu(fileName = "New Data Information", menuName = "ScriptableObjects/DataInformation")]
public class DataInformation : ScriptableObject
{
    [Header("Money")]
    public int money;


    [Header("Popularity")]
    public int popularity;
    public int levelPopularity;
    public int maxLevelPopularity;

    [Header("Capacity")]
    public int levelCapacity;
    public int maxLevelCapacity;

    [Header("Keuangan")]
    public int levelKeuangan;
    public int maxLevelKeuangan;

    [Header("Keamanan")]
    public int levelKeamanan;
    public int maxLevelKeamanan;



    [Header("Event")]
    public int counterDay;
    public bool eventMajorMixUhey;
    //public bool eventMajorKleptomania;
    public bool eventMajorKucingMalang;
    //public bool eventMajorPaketKucing;
}