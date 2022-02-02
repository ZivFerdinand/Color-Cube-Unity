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

    public class Functions
    {
        /// <summary>
        /// Load Scriptable Objects' Data From Folder
        /// </summary>
        /// <typeparam name="GameData"></typeparam>
        /// <param name="dataSize">Current Data Amount</param>
        /// <param name="listTabData">The List that Will Be Updated Later</param>
        /// <param name="dataPath">Folder Path</param>
        public static void LoadGameData<GameData>(ref int dataSize, List<GameData> listTabData, string dataPath) where GameData : ScriptableObject
        {
            GameData[] list = Resources.LoadAll<GameData>(dataPath);
            dataSize = list.Length;

            foreach (GameData gd in list)
            {
                listTabData.Add(gd);
            }
        }
    }
}