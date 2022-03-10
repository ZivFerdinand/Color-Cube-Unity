using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();

    [SerializeField] 
    private ColorMap colorPallete;
    
    [SerializeField] 
    private float startAnimationDuration = 0.5f;

    private bool[] levelColorChecker;

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

    private GameObject[] animatedGameObject_0;
    private GameObject[] animatedGameObject_1;
    private GameObject parentGameObject;


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
            Color x = Database.Functions.ColorEnumToColorUnity(levelData[currentSelectedLevel].cubeSidesColor[i], colorPallete);
            
            cubeSideChildren[i].materials[0].color = x;
        }

        //Set Frame Tiles Color
        for (int i = 1; i < cubeTileChildren.Length; i++)
        {
            Color x = Database.Functions.ColorEnumToColorUnity(levelData[currentSelectedLevel].tileColor[i - 1], colorPallete);
            
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
                cubeTileChildren[i].material.SetColor("_Color", colorPallete.colors[0]);
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

        GameManagerLoader();
        PlayStartAnimation();
    }

    public void  Update()
    {
        int touchedPlane = CheckChildCollide.collidedTileIndex;
        int touchedCubeSide = TouchingCubeArea.touchingSideIndex;

        if(levelData[currentSelectedLevel].tileColor[touchedPlane] == levelData[currentSelectedLevel].cubeSidesColor[touchedCubeSide])
        {
            if(!levelColorChecker[touchedPlane])
            {
                Debug.Log(levelData[currentSelectedLevel].tileColor[touchedPlane] + "s are touching");
                untouchedColor--;
                levelStatusUIChildren[levelStatusUI_NextIndex++].color = Database.Functions.ColorEnumToColorUnity(levelData[currentSelectedLevel].tileColor[touchedPlane], colorPallete);
            }
            levelColorChecker[touchedPlane] = true;
            
        }
    }

    private void GameManagerLoader()
    {
        animatedGameObject_0 = GameObject.FindGameObjectsWithTag("Animation_0");

        parentGameObject = GameObject.FindGameObjectWithTag("Parent");

        animatedGameObject_1 = GameObject.FindGameObjectsWithTag("Animation_1");
    }
    private void PlayStartAnimation()
    {
        foreach(GameObject anim in animatedGameObject_1)
        {
            anim.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        
        parentGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        parentGameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        parentGameObject.transform.LeanRotate(new Vector3(0, 0, 0), startAnimationDuration).setEaseInOutElastic();
        parentGameObject.transform.LeanScale(Vector3.one, startAnimationDuration).setEaseOutBounce().setOnComplete(async () =>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CubeMovement>().FallingAnimation();
            foreach (GameObject anim in animatedGameObject_0)
            {  
                anim.transform.SetParent(null);
            }
            Destroy(parentGameObject);
            StartCoroutine(UIAnimation());
        });

    }
    private IEnumerator UIAnimation()
    {
        foreach (GameObject anim in animatedGameObject_1)
        {
            anim.transform.LeanScale(Vector3.one, Mathf.Sqrt(startAnimationDuration)).setEaseOutBounce();
            yield return new WaitForSeconds(0.3f);
        }   
    }
}