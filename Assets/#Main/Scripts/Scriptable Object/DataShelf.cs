using UnityEngine;

[CreateAssetMenu(fileName = "New Data Shelf", menuName = "ScriptableObjects/DataShelf")]
public class DataShelf : ScriptableObject
{
    public Mainan mainan;
    public string nameMainan;
    public string descMainan;

    public int price;
    public int stok;
    public int timerRestock;
    public int priceToUnlock;

    public int levelPrice;
    public int levelRestock;
}