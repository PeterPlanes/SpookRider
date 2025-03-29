using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float lookSpeedX = 2f;  // speed left Right
    public float lookSpeedY = 2f;  // speed Up Down
    public float minRotationX = -90f;  // Angle Low
    public float maxRotationX = 90f;   // Angle High

    private float currentRotationX = 0f;  // Up Down
    private float currentRotationY = 0f;  // Left Right

    private float minRotationY = -90f;  // Left
    private float maxRotationY = 90f;   // Right

    void Update()
    {
        // Left Right Mouse
        float mouseX = Input.GetAxis("Mouse X");
        // Up Down Mouse
        float mouseY = Input.GetAxis("Mouse Y");

        //Angle Left Right
        currentRotationY += mouseX * lookSpeedX;

        //  Angle <180 องศา
        currentRotationY = Mathf.Clamp(currentRotationY, minRotationY, maxRotationY);

        // Cal Up Down
        currentRotationX -= mouseY * lookSpeedY;
        currentRotationX = Mathf.Clamp(currentRotationX, minRotationX, maxRotationX);

        transform.localRotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
    }
}
