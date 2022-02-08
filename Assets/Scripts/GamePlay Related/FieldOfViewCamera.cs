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
        //Get This GameObject Slider Value then Replace With Last Saved Value
        transform.GetComponent<Slider>().value = Database.Cameras.lastFOVValue;
    }

    public void onSliderChanged()
    {
        //Current Updating Slider Value
        float currentValue = transform.GetComponent<Slider>().value;
        
        //Keep Current FOV Value
        Database.Cameras.lastFOVValue = currentValue;

        //Update Main Camera FOV Value Directly
        Camera.main.fieldOfView = 75 - (currentValue * 75);
    }
}