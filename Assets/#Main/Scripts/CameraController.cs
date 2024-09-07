using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isCameraCanMoving;

    public Camera mainCamera;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public float zoomSpeed; // Kecepatan zoom
    public float minZoom; // Ukuran minimum kamera
    public float maxZoom; // Ukuran maksimum kamera

    void Update()
    {
        if (!isCameraCanMoving) return;

        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;


        //Camera Size
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        // Jika menggunakan kamera ortografis
        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize -= scrollData * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
        // Jika menggunakan kamera perspektif
        else
        {
            mainCamera.fieldOfView -= scrollData * zoomSpeed;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minZoom, maxZoom);
        }
    }
}
