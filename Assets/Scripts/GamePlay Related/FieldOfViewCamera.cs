using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfViewCamera : MonoBehaviour
{
    private float sliderValue;
    private int cameraFOVValue;

    void Awake()
    {
        transform.GetComponent<Slider>().value = Database.Cameras.lastFOVValue;
    }
    private void Update()
    {
    }

    public void onSliderChanged()
    {
        float currentValue = transform.GetComponent<Slider>().value;
        
        Database.Cameras.lastFOVValue = currentValue;
        Camera.main.fieldOfView = 75 - (currentValue * 75);
    }
}