using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Color-Cube/Create Level Asset", fileName = "Level_")]

public class LevelScriptableObject : ScriptableObject
{
    public string levelName;
    public int levelNumber;
    public int gridSize;
    [SerializeField]
    public TileData[] tileData;
    [SerializeField]
    public TileColor[] tileColor;
    [SerializeField]
    public TileColor[] cubeSidesColor = new TileColor[6];

    public void OnValidate()
    {
        if(tileData.Length != Mathf.Pow(gridSize, 2))
        {
            tileData = new TileData[(int) Mathf.Pow(gridSize, 2)];
        }
        if(tileColor.Length != Mathf.Pow(gridSize, 2))
        {
            tileColor = new TileColor[(int) Mathf.Pow(gridSize, 2)];
        }
        if(cubeSidesColor.Length != 6)
        {
            cubeSidesColor = new TileColor[6];
        }
    }
    public void OnEnable()
    {
        if (levelName == null)
        {
            Init();
        }
    }

    public void Init()
    {
        levelName = "The Opening";
        levelNumber = 0;
        gridSize = 2;
        tileData = new TileData[(int) Mathf.Pow(gridSize, 2)];
        tileColor = new TileColor[(int)Mathf.Pow(gridSize, 2)];
    }
    
}