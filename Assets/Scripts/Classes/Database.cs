using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    
    public class Cameras
    {
        public static bool isInverted
        {
            set
            {
                PlayerPrefs.SetInt("camInverted", (value == true) ? 1 : 0);
            }
            get
            {
                int temp = PlayerPrefs.GetInt("camInverted", 0);
                return (temp == 1) ? true : false;
            }
        }
    }
}