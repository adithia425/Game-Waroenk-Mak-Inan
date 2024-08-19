using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Debug.Log("Button Clicked!"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
