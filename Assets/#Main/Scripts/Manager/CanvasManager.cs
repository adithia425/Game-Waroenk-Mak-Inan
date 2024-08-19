using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;


    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textPopularity;
    public TextMeshProUGUI textNamePopularity;


    public TextMeshProUGUI textTimeDaily;

    public List<ShelfUI> listShelfUI;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetShelfUI(int index, bool con)
    {
        listShelfUI[index].gameObject.SetActive(con);
    }

    public void SetTextMoney(int val)
    {
        textMoney.text = val.ToString();
    }
    public void SetTextTime(string val)
    {
        Debug.Log(val);
        textTimeDaily.text = val;
    }

    public void SetTextPopularity(int val, int targetPopularitas)
    {
        textPopularity.text = val + "/" + targetPopularitas;
    }

    public void SetTextNamePopularity(int level)
    {
        textNamePopularity.text = "Lv. " + level + " " + GameManager.instance.database.GetNamePopularity(level);
    }
}
