using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSlider : MonoBehaviour
{
    public Vector3 newPosition = new Vector3(600, 0, 0);

    public bool isOnLeft;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSlide()
    {
        isOnLeft = !isOnLeft;

        RectTransform rectTransform = GetComponent<RectTransform>();

        if (isOnLeft)
        {
            rectTransform.anchoredPosition = Vector3.zero;
        }
        else
        {
            rectTransform.anchoredPosition = newPosition;
        }
    }

}
