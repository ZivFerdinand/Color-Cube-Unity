using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Database;

public class CameraRotator : MonoBehaviour
{
    private float targetRotation = 0;


    public Button cameraButton;
    public float rotateDuration;
    void Start()
    {
        cameraButton.enabled = true;

        //Get Last Target Rotation
        targetRotation = (Database.Cameras.isInverted) ? 180 : 0;

        //Set Rotation to Last Saved Angle
        transform.eulerAngles = new Vector3(0, targetRotation, 0);
    }
    public void onCameraButtonClick()
    {
        cameraButton.enabled = false;

        //Switch Target Rotation to Another Angle (0 or 180) Once Button Clicked
        targetRotation = (targetRotation == 0) ? 180 : 0;

        //Rotate Camera to the Desired Angle
        transform.LeanRotateY(targetRotation, rotateDuration).setEaseInOutSine().setOnComplete(() =>
        {
            cameraButton.enabled = true; 

            //Switch Camera Angle Status
            Database.Cameras.isInverted = !Database.Cameras.isInverted;
        });
    }
}
