using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public float timeDelay;
    void Start()
    {
        Invoke(nameof(ChangeScene), timeDelay);
    }

    public void ChangeScene()
    {
        ScenesManager.instance.ChangeScene("Development");
    }
}
