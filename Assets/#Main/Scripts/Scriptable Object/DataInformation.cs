using UnityEngine;

[CreateAssetMenu(fileName = "New Data Information", menuName = "ScriptableObjects/DataInformation")]
public class DataInformation : ScriptableObject
{
    public int money;
    public int popularity;
    public int levelPopularity;
    public int levelCapacity;
}