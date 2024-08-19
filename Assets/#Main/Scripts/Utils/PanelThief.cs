using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelThief : MonoBehaviour
{

    public GameObject npc;
    public RectTransform dot; // The RectTransform of the moving dot
    public float tolerance; // Tolerance for considering the dot to be in the middle

    public void CheckDotPosition()
    {
        NpcController npcStun = npc.GetComponent<NpcController>();

        // Check if the dot is in the middle
        float dotPositionX = dot.anchoredPosition.x;
        if (Mathf.Abs(dotPositionX) <= tolerance)
        {
            Debug.Log("OK");
            npcStun.ActionThiefFailed();
        }
        else
        {
            Debug.Log("Gagal");
            npcStun.ActionThiefSuccess();
        }

        gameObject.SetActive(false);
    }
}
