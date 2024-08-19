using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDot : MonoBehaviour
{
    public RectTransform dot; // The RectTransform of the moving dot
    public float speed = 500f; // Speed of the moving dot
    public float leftLimit = -200f; // Left limit of movement
    public float rightLimit = 200f; // Right limit of movement
    private bool movingRight = true; // Direction of movement

    void Update()
    {
        // Move the dot
        if (movingRight)
        {
            dot.anchoredPosition += Vector2.right * speed * Time.deltaTime;
            if (dot.anchoredPosition.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            dot.anchoredPosition += Vector2.left * speed * Time.deltaTime;
            if (dot.anchoredPosition.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}
