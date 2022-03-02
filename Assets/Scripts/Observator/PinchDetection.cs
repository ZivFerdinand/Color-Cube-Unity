using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchDetection : MonoBehaviour
{
    private TouchControls controls;
    private Coroutine zoomCoroutine;

    public float zoomSpeed;

    private void Awake()
    {
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
                Camera.main.fieldOfView += 1 * zoomSpeed * Time.deltaTime;
            }
            else if(distance<previousDistance)
            {
                Camera.main.fieldOfView -= 1 * zoomSpeed * Time.deltaTime;
            }

            previousDistance = distance;
            yield return null;
        }
    }
}
