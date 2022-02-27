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
        public static float lastFOVValue
        {
            set
            {
                PlayerPrefs.SetFloat("camFOV", value);
            }
            get
            {
                return PlayerPrefs.GetFloat("camFOV", 0);
            }
        }
    }

    public class LevelRelated
    {
        public static int selectedLevelFromScene
        {
            set
            {
                PlayerPrefs.SetInt("sceneLevel", value);
            }
            get
            {
                return PlayerPrefs.GetInt("sceneLevel", 0);
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

        /// <summary>
        /// Tile Color Enum to Color Variable
        /// </summary>
        /// <param name="tileColor">TileColor</param>
        /// <returns></returns>
        public static Color ColorEnumToColorUnity(TileColor tileColor)
        {
            Color y = new Color();
            switch(tileColor)
            {
                case TileColor.Blue:
                    y = Color.blue;
                    break;
                case TileColor.Green:
                    y = Color.green;
                    break;
                case TileColor.Orange:
                    y = Color.red;
                    break;
                case TileColor.Red:
                    y = Color.red;
                    break;
                case TileColor.White:
                    y = Color.white;
                    break;
                case TileColor.Yellow:
                    y = Color.yellow;
                    break;
            }
            return y;
        }
    }
}