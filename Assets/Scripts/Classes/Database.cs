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
                return PlayerPrefs.GetFloat("camFOV", 61);
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
        public static int gridLevelSize
        {
            set
            {
                PlayerPrefs.SetInt("gridSize", value);
            }
            get
            {
                return PlayerPrefs.GetInt("gridSize", 0);
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
        /// Tile Color Enum to Color Variable (String)
        /// </summary>
        /// <param name="tileColor">TileColor Enum</param>
        /// <returns></returns>
        public static string ColorEnumToColorUnity(TileColor tileColor)
        {
            string y = "";
            switch(tileColor)
            {
                case TileColor.First:
                    y = "First Color";
                    break;
                case TileColor.Second:
                    y = "Second Color";
                    break;
                case TileColor.Third:
                    y = "Third Color";
                    break;
                case TileColor.Forth:
                    y = "Forth Color";
                    break;
                case TileColor.Fifth:
                    y = "Fifth Color";
                    break;
                case TileColor.Sixth:
                    y = "Sixth Color";
                    break;
            }
            return y;
        }
        
        /// <summary>
        /// Tile Color Enum to Color Variable
        /// </summary>
        /// <param name="tileColor">TileColor Enum</param>
        /// <returns></returns>
        public static Color ColorEnumToColorUnity(TileColor tileColor, ColorMap colorPallete)
        {
            Color y = new Color();
            switch(tileColor)
            {
                case TileColor.First:
                    y = colorPallete.colors[1];
                    break;
                case TileColor.Second:
                    y = colorPallete.colors[2];
                    break;
                case TileColor.Third:
                    y = colorPallete.colors[3];
                    break;
                case TileColor.Forth:
                    y = colorPallete.colors[4];
                    break;
                case TileColor.Fifth:
                    y = colorPallete.colors[5];
                    break;
                case TileColor.Sixth:
                    y = colorPallete.colors[6];
                    break;
                default:
                    y = colorPallete.colors[0];
                    break;
            }
            return y;
        }

        /// <summary>
        /// Direction in InputManager Enum to Vector2
        /// </summary>
        /// <param name="direction">Direction Enum</param>
        /// <returns></returns>
        public static Vector2 DirectionEnumToVectorUnity(InputManager.Direction direction)
        {
            Vector2 y = new Vector2();
            switch(direction)
            {
                case InputManager.Direction.Down:
                    y = Vector2.down;
                    break;
                case InputManager.Direction.Up:
                    y = Vector2.up;
                    break;
                case InputManager.Direction.Right:
                    y = Vector2.right;
                    break;
                case InputManager.Direction.Left:
                    y = Vector2.left;
                    break;
                default:
                    y = Vector2.zero;
                    break;
            }

            return y;
        }

        /// <summary>
        /// Check Whether a Number is In The Given Range (Inclusively)
        /// </summary>
        /// <param name="low">Lowest Range</param>
        /// <param name="high">Highest Range</param>
        /// <param name="toBeChecked">The Spesific Variable To Be Checked</param>
        /// <returns></returns>
        public static bool InRangeInclusive(float low, float high, float toBeChecked)
        {
            return (toBeChecked >= low && toBeChecked <= high) ? true : false;
        }

    }
}