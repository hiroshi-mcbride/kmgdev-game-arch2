using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerCam
{
    public float sensX = 240.0f;
    public float sensY = 240.0f;

    public Transform orientation;
    private Transform CameraTrans;

    private float xRotation;
    private float yRotation;


    public NewPlayerCam(Transform _camTransform, Transform _playerOrientation)
    {
        CameraTrans = _camTransform;
        orientation = _playerOrientation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdatingCamera()
    {
        //Debug.Log("playerorientatation = " + orientation);
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        CameraTrans.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }


}