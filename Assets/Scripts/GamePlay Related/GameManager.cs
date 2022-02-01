using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();
    private int levelTotal;
    private MeshRenderer[] cubeTileChildren;
    private MeshRenderer[] cubeSideChildren;

    public GameObject cubeTile;
    public GameObject cubeSides;

    public void Awake()
    {
        LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");

        cubeSideChildren = cubeSides.GetComponentsInChildren<MeshRenderer>();
        cubeTileChildren = cubeTile.GetComponentsInChildren<MeshRenderer>();

        for (int i = 1; i < cubeSideChildren.Length;i++)
        {
            Color x = new Color();
            switch (levelData[0].cubeSidesColor[i - 1])
            {
                case TileColor.Blue:
                    x = Color.blue;
                    break;
                case TileColor.Green:
                    x = Color.green;
                    break;
                case TileColor.Orange:
                    x = Color.red;
                    break;
                case TileColor.Red:
                    x = Color.red;
                    break;
                case TileColor.White:
                    x = Color.white;
                    break;
                case TileColor.Yellow:
                    x = Color.yellow;
                    break;
            }
            
            cubeSideChildren[i].material.SetColor("_Color", x);
        }

        for (int i = 1; i < cubeTileChildren.Length; i++)
        {
            Color x = new Color();
            switch (levelData[0].tileColor[i - 1])
            {
                case TileColor.Blue:
                    x = Color.blue;
                    break;
                case TileColor.Green:
                    x = Color.green;
                    break;
                case TileColor.Orange:
                    x = Color.red;
                    break;
                case TileColor.Red:
                    x = Color.red;
                    break;
                case TileColor.White:
                    x = Color.white;
                    break;
                case TileColor.Yellow:
                    x = Color.yellow;
                    break;
            }
            if (levelData[0].tileData[i - 1] == TileData.Color)
                cubeTileChildren[i].material.SetColor("_Color", x);
        }
    }




    /// <summary>
    /// Load Scriptable Objects' Data From Folder
    /// </summary>
    /// <typeparam name="GameData"></typeparam>
    /// <param name="dataSize">Current Data Amount</param>
    /// <param name="listTabData">The List that Will Be Updated Later</param>
    /// <param name="dataPath">Folder Path</param>
    public void LoadGameData<GameData>(ref int dataSize, List<GameData> listTabData, string dataPath) where GameData : ScriptableObject
    {
        GameData[] list = Resources.LoadAll<GameData>(dataPath);
        dataSize = list.Length;

        foreach (GameData gd in list)
        {
            listTabData.Add(gd);
        }
    }
}