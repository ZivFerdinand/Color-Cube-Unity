using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();
    private bool[] levelColorChecker;

    [SerializeField] private ColorMap colorPallete;
    private int levelTotal;
    private int untouchedColor;
    private int currentSelectedLevel;
    private int levelStatusUI_NextIndex;
    private MeshRenderer[] cubeTileChildren;
    private MeshRenderer[] cubeSideChildren;
    private Image[] levelStatusUIChildren;

    public GameObject cubeTile;
    public GameObject cubeSides;

    public GameObject levelStatusUI;
    public GameObject panelUI;
    [SerializeField] private float startAnimationDuration = 0.5f;

    public void Start()
    {
        Debug.Log("Level: " + currentSelectedLevel);
        currentSelectedLevel = Database.LevelRelated.selectedLevelFromScene;
        untouchedColor = levelStatusUI_NextIndex = 0;

        Database.Functions.LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");
        levelColorChecker = new bool[levelData[currentSelectedLevel].tileData.Length];

        Database.LevelRelated.gridLevelSize = levelData[currentSelectedLevel].gridSize;

        levelStatusUIChildren = levelStatusUI.GetComponentsInChildren<Image>();
        cubeSideChildren = cubeSides.GetComponentsInChildren<MeshRenderer>();
        cubeTileChildren = cubeTile.GetComponentsInChildren<MeshRenderer>();

        //Set Cube Sides Color
        for (int i = 0; i < cubeSideChildren.Length;i++)
        {
            Color x = new Color();
            switch (levelData[currentSelectedLevel].cubeSidesColor[i])
            {
                case TileColor.Blue:
                    x = colorPallete.colors[1];
                    break;
                case TileColor.Green:
                    x = colorPallete.colors[2];
                    break;
                case TileColor.Orange:
                    x = colorPallete.colors[3];
                    break;
                case TileColor.Red:
                    x = colorPallete.colors[4];
                    break;
                case TileColor.White:
                    x = colorPallete.colors[5];
                    break;
                case TileColor.Yellow:
                    x = colorPallete.colors[6];
                    break;
                default:
                    x = colorPallete.colors[0];
                    break;
            }
            
            cubeSideChildren[i].materials[0].color = x;
        }

        //Set Frame Tiles Color
        for (int i = 1; i < cubeTileChildren.Length; i++)
        {
            Color x = new Color();
            switch (levelData[currentSelectedLevel].tileColor[i - 1])
            {
                case TileColor.Blue:
                    x = colorPallete.colors[1];
                    break;
                case TileColor.Green:
                    x = colorPallete.colors[2];
                    break;
                case TileColor.Orange:
                    x = colorPallete.colors[3];
                    break;
                case TileColor.Red:
                    x = colorPallete.colors[4];
                    break;
                case TileColor.White:
                    x = colorPallete.colors[5];
                    break;
                case TileColor.Yellow:
                    x = colorPallete.colors[6];
                    break;
                default:
                    x = colorPallete.colors[0];
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
                cubeTileChildren[i].material.SetColor("_Color", Color.white);
            }
        }


        for (int i = 0; i < levelStatusUIChildren.Length; i++)
        {
            if (i < untouchedColor)
            {
                GameObject.Find("Bar (" + i).gameObject.SetActive(true);
                levelStatusUIChildren[i].color = Color.black;
            }
            else
            {
                GameObject.Find("Bar (" + i).gameObject.SetActive(false);
            }
        }

        PlayStartAnimation();
    }

    public void PlayStartAnimation()
    {
        GameObject[] animatedObject = GameObject.FindGameObjectsWithTag("anim1");
        GameObject parent = GameObject.FindGameObjectWithTag("parent");

        GameObject[] animatedObjectt = GameObject.FindGameObjectsWithTag("anim2");
        
        foreach(GameObject anim in animatedObjectt)
        {
            anim.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        parent.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        parent.transform.rotation = Quaternion.Euler(0, -180, 0);
        parent.transform.LeanRotate(new Vector3(0, 0, 0), startAnimationDuration).setEaseInOutElastic();
        parent.transform.LeanScale(Vector3.one, startAnimationDuration).setEaseOutBounce().setOnComplete(async () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().FallingAnimation();
            foreach (GameObject anim in animatedObject)
            {  
                anim.transform.SetParent(null);
            }
            Destroy(parent);
            StartCoroutine(a());
        });

    }
    IEnumerator a()
    {
        
        GameObject[] animatedObjectt = GameObject.FindGameObjectsWithTag("anim2");

        foreach (GameObject anim in animatedObjectt)
        {
            anim.transform.LeanScale(Vector3.one, startAnimationDuration).setEaseOutBounce();
            yield return new WaitForSeconds(2);
        }   
    }


    public void  Update()
    {
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
                levelStatusUIChildren[levelStatusUI_NextIndex++].color = Database.Functions.ColorEnumToColorUnity(levelData[currentSelectedLevel].tileColor[touchedPlane]);
            }
            levelColorChecker[touchedPlane] = true;
            
        }
    }
}