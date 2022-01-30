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
        targetRotation = (targetRotation == 0) ? 180 : 0;
    }
}
