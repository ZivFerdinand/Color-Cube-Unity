using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchDetection : MonoBehaviour
{
    private TouchControls controls;
    private Coroutine zoomCoroutine;
    private Camera fovValue;

    public float zoomSpeed;

    private void Awake()
    {
        fovValue = Camera.main;
        fovValue.fieldOfView = Database.Cameras.lastFOVValue;
        
        controls = new TouchControls();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Touch.SecondaryTouchContact.started += _ => ZoomStart();
        controls.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
    }

    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(ZoomDetection());
    }

    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator ZoomDetection()
    {
        float previousDistance = 0f, distance = 0f;

        while(true)
        {
            distance = Vector2.Distance(controls.Touch.PrimaryFingerPosition.ReadValue<Vector2>(),
            controls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

            if(distance>previousDistance)
            {
                fovValue.fieldOfView += 1 * zoomSpeed * Time.deltaTime;
            }
            else if(distance<previousDistance)
            {
                fovValue.fieldOfView -= 1 * zoomSpeed * Time.deltaTime;
            }

            //Keep Current FOV Value
            Database.Cameras.lastFOVValue = fovValue.fieldOfView;

            previousDistance = distance;
            yield return null;
        }
    }
}
