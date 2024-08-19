using UnityEngine;

[CreateAssetMenu(fileName = "New List Data Shelf", menuName = "ScriptableObjects/ListDataShelf")]
public class ListDataShelf : ScriptableObject
{
    public int levelShelf;
    public DataShelf[] dataShelf;
}