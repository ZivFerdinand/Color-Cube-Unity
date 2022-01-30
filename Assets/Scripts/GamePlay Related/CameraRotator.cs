using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Database;

public class CameraRotator : MonoBehaviour
{
    float targetRotation = 0;

    public float rotateSpeed;
    void Start()
    {
        targetRotation = (Database.Cameras.isInverted) ? 180 : 0;
        transform.eulerAngles = new Vector3(0, targetRotation, 0);
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.eulerAngles.y - targetRotation) > 1)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }

    public void onCameraButtonClick()
    {
        Database.Cameras.isInverted = !Database.Cameras.isInverted;
        targetRotation = (targetRotation == 0) ? 180 : 0;
    }
}
