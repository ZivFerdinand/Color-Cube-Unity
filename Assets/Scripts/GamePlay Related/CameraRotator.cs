using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Database;

public class CameraRotator : MonoBehaviour
{
     Touch initTouch = new Touch();
     Vector3 origRot;
     float dir = -1;
     float rotY = 0f;


    public TextMeshProUGUI cameraStatus;
    public Camera cam;
    public float rotateSpeed;
    void Start()
    {
        origRot = cam.transform.eulerAngles;
        rotY = origRot.y;
    }
    void FixedUpdate()
    {
        if(Database.GameSettings.allowCameraMovement)
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    initTouch = touch;
                }
                else if(touch.phase == TouchPhase.Moved)
                {
                    float deltaX = initTouch.position.x - touch.position.x;

                    deltaX = (deltaX > 0) ? 1 : -1;

                    rotY = deltaX * Time.deltaTime * rotateSpeed * dir;
                    transform.Rotate(0, rotY, 0);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }

        }

        cameraStatus.text = "Camera " + ((Database.GameSettings.allowCameraMovement) ? "On" : "Off");
    }

    public void onCameraButtonClick()
    {
        Database.GameSettings.allowCameraMovement = !Database.GameSettings.allowCameraMovement;
    }
}