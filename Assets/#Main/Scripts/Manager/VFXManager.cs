using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [Header("VFX Prefab")]
    public GameObject vfxPrefab; // Prefab dari VFX yang akan digunakan

    [Header("VFX Pool")]
    public int initialPoolSize = 10; // Ukuran awal pool
    private List<GameObject> vfxPool; // List untuk menyimpan objek VFX yang di-pool

    void Start()
    {
        // Inisialisasi pool dengan ukuran awal
        vfxPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject vfx = Instantiate(vfxPrefab);
            vfx.SetActive(false);
            vfxPool.Add(vfx);
        }
    }

    public GameObject GetVFX(Transform pos)
    {
        // Mencari VFX yang tidak aktif di pool
        foreach (GameObject vfx in vfxPool)
        {
            if (!vfx.activeInHierarchy)
            {
                vfx.transform.position = pos.position;
                vfx.transform.rotation = pos.rotation;
                vfx.SetActive(true);
                return vfx;
            }
        }

        // Jika semua VFX dalam pool aktif, instansiasi VFX baru
        GameObject newVFX = Instantiate(vfxPrefab, pos.position, pos.rotation);
        newVFX.transform.parent = pos;
        vfxPool.Add(newVFX);
        return newVFX;
    }

    public void ReturnVFX(GameObject vfx)
    {
        // Mengembalikan VFX ke pool (menonaktifkannya)
        vfx.SetActive(false);
    }
}
