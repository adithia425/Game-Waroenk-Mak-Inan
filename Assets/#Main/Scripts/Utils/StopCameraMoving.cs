using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCameraMoving : MonoBehaviour
{


    private void OnEnable()
    {
        if (GameManager.instance)
        {
            //Debug.Log("Stop Move", gameObject);
            GameManager.instance.cameraController.isCameraCanMoving = false;
        }
    }

    private void OnDisable()
    {
        if(GameManager.instance)
        GameManager.instance.cameraController.isCameraCanMoving = true;
    }
}
