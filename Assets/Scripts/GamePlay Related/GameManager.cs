using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();
    private int levelTotal;
    private MeshRenderer[] cubeTileChildren;

    public GameObject cubeTile;


    public void Awake()
    {
        LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");

        cubeTileChildren = cubeTile.GetComponentsInChildren<MeshRenderer>();


        for (int i = 1; i < cubeTileChildren.Length;i++)
        {
            // NEED TO BE UPDATED LATER, ONLY 2 COLORS CHANGE AVAILABLE

            Color x;
            if(levelData[0].tileColor[i-1] == TileColor.Red)
            {
                x = Color.red;
            }
            else
            {
                x = Color.black;
            }
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