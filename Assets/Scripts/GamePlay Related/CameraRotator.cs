using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Database;

public class CameraRotator : MonoBehaviour
{
    float targetRotation = 0;
    private Button cameraButton;


    public float rotateDuration;
    void Start()
    {
        cameraButton = GameObject.Find("CameraButton").GetComponent<Button>();
        cameraButton.enabled = true;
        targetRotation = (Database.Cameras.isInverted) ? 180 : 0;
        transform.eulerAngles = new Vector3(0, targetRotation, 0);
    }
    public void onCameraButtonClick()
    {
        cameraButton.enabled = false;
        targetRotation = (targetRotation == 0) ? 180 : 0;
        transform.LeanRotateY(targetRotation, rotateDuration).setEaseInOutSine().setOnComplete(() =>
        {
            cameraButton.enabled = true; 
            Database.Cameras.isInverted = !Database.Cameras.isInverted;
        });
    }
}
