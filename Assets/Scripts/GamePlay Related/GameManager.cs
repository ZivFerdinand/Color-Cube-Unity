using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();
    private bool[] levelColorChecker;
    private int levelTotal;
    private int untouchedColor;
    private int currentSelectedLevel;
    private MeshRenderer[] cubeTileChildren;
    private MeshRenderer[] cubeSideChildren;


    public TextMeshProUGUI untouchedColorStatusUI;
    public GameObject cubeTile;
    public GameObject cubeSides;

    public void Awake()
    {
        currentSelectedLevel = Database.LevelRelated.selectedLevelFromScene;
        untouchedColor = 0;

        Database.Functions.LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");
        levelColorChecker = new bool[levelData[currentSelectedLevel].tileData.Length];

        cubeSideChildren = cubeSides.GetComponentsInChildren<MeshRenderer>();
        cubeTileChildren = cubeTile.GetComponentsInChildren<MeshRenderer>();
        
        //Set Cube Sides Color
        for (int i = 0; i < cubeSideChildren.Length;i++)
        {
            Color x = new Color();
            switch (levelData[currentSelectedLevel].cubeSidesColor[i])
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

        //Set Frame Tiles Color
        for (int i = 1; i < cubeTileChildren.Length; i++)
        {
            Color x = new Color();
            switch (levelData[currentSelectedLevel].tileColor[i - 1])
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

            if (levelData[currentSelectedLevel].tileData[i - 1] == TileData.Color)
            {
                levelColorChecker[i - 1] = false;

                //Set Level Left Colors that Are Untouched
                untouchedColor++;

                cubeTileChildren[i].material.SetColor("_Color", x);
            }
            else
            {
                levelColorChecker[i - 1] = true;
                cubeTileChildren[i].material.SetColor("_Color", Color.clear);
            }
        }
    }


    public void  Update()
    {
        Debug.Log(currentSelectedLevel);
        untouchedColorStatusUI.text = "Untouched Colors:\n" + untouchedColor.ToString();
        int touchedPlane = CheckChildCollide.collidedTileIndex;
        int touchedCubeSide = TouchingCubeArea.touchingSideIndex;

        //Debug.Log("TileColor: "+levelData[0].tileColor[touchedPlane] + touchedPlane.ToString());
        //Debug.Log("CubeSidesColor: "+levelData[0].cubeSidesColor[touchedCubeSide] + touchedCubeSide.ToString());
        //Debug.Log(untouchedColor);

        if(levelData[currentSelectedLevel].tileColor[touchedPlane] == levelData[currentSelectedLevel].cubeSidesColor[touchedCubeSide])
        {
            if(!levelColorChecker[touchedPlane])
            {
                Debug.Log(levelData[currentSelectedLevel].tileColor[touchedPlane].ToString() + "s are touching");
                untouchedColor--;
            }
            levelColorChecker[touchedPlane] = true;
            
        }
    }


    
}