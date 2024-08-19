using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    public int level = 1;
    public int maxLevel = 5;
    public int[] upgradeCosts = { 100, 200, 300, 400, 500 }; // Biaya upgrade untuk setiap level
    public int currentRupiah = 1000; // Mata uang awal

    void Start()
    {
        // Inisialisasi level dan status objek MainHall
        UpdateMainHall();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            UpgradeMainHall();
        }
    }
    public void UpgradeMainHall()
    {
        if (level < maxLevel && currentRupiah >= upgradeCosts[level - 1])
        {
            currentRupiah -= upgradeCosts[level - 1];
            level++;
            UpdateMainHall();
            Debug.Log("MainHall upgraded to level " + level);
        }
        else
        {
            Debug.Log("Not enough rupiah to upgrade or max level reached.");
        }
    }

    void UpdateMainHall()
    {
        // Logika untuk mengubah penampilan atau kemampuan MainHall sesuai level
        // Misalnya, mengubah ukuran atau warna
        transform.localScale = Vector3.one * (1 + level * 0.5f);
        GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, (float)level / maxLevel);
    }
}
