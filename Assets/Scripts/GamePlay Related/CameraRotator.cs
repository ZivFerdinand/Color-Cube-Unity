using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Database;

public class CameraRotator : MonoBehaviour
{
    private float targetRotation = 0;
    private float rotateDuration;


    public Button cameraButton;

    void Start()
    {
        rotateDuration = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AnimationManager>().cameraRotateDuration;
        cameraButton.enabled = true;

        //Get Last Target Rotation
        targetRotation = (Database.Cameras.IsInverted) ? 180 : 0;

        //Set Rotation to Last Saved Angle
        transform.eulerAngles = new Vector3(0, targetRotation, 0);
    }
    
    public void OnCameraButtonClick()
    {
        cameraButton.enabled = false;

        //Switch Target Rotation to Another Angle (0 or 180) Once Button Clicked
        targetRotation = (targetRotation == 0) ? 180 : 0;

        //Rotate Camera to the Desired Angle
        transform.LeanRotateY(targetRotation, rotateDuration).setEaseInOutSine().setOnComplete(() =>
        {
            cameraButton.enabled = true; 

            //Switch Camera Angle Status
            Database.Cameras.IsInverted = !Database.Cameras.IsInverted;
        });
    }
}
