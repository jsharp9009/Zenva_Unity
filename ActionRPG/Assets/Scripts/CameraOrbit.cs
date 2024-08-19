using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public float lookSensitivity;
    public float minXLook;
    public float maxXLook;
    public Transform camAnchor;
    public bool invertX;
    private float currentXRotation;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate(){
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.eulerAngles += Vector3.up * x * lookSensitivity;

        if(invertX)
            currentXRotation += y * lookSensitivity;
        else
            currentXRotation -= y * lookSensitivity;

        currentXRotation = Mathf.Clamp(currentXRotation, minXLook, maxXLook);

        var clamped = camAnchor.eulerAngles;
        clamped.x = currentXRotation;
        camAnchor.eulerAngles = clamped;
    }
}
