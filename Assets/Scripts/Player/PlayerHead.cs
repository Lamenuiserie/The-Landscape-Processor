using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerHead : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;


    public Transform cursor;


    private Camera mainCamera;


    void Start ()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update ()
    {
        //transform.LookAt(cursor);
        Ray ray = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        Quaternion rotationToMake = Quaternion.FromToRotation(transform.forward, ray.direction);
        if (rotationToMake.eulerAngles.y >= 324 && rotationToMake.eulerAngles.y <= 335)
        {
            Debug.Log(transform.rotation.eulerAngles);
            transform.rotation = rotationToMake;
        }
    }
}
