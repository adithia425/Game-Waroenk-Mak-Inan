using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCamera : MonoBehaviour
{
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
