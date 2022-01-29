using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
   public static class GameSettings
    {        
        public static bool allowCameraMovement
        {
            set
            {
                PlayerPrefs.SetInt("cameraMovement", (value == true) ? 1 : 0);
            }
            get
            {
                int temp = PlayerPrefs.GetInt("cameraMovement", 0);
                return (temp == 1) ? true : false;
            }
        }
    }
}