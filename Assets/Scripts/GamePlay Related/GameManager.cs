using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private float startAnimationDuration = 0.5f;
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();

    [SerializeField] 
    private ColorMap colorPallete;
    
    

    private bool[] levelColorChecker;

    private int levelTotal;
    private int currentLevelProgress;
    private int untouchedColor;
    private int currentSelectedLevel;

    private MeshRenderer[] cubeTileChildren;
    private MeshRenderer[] cubeSideChildren;

    private Image[] levelStatusUIChildren;

    public GameObject cubeTile;
    public GameObject cubeSides;
    public GameObject levelStatusUI;
    public GameObject panelUI;
    public GameObject cubePlayer;

    private GameObject[] animatedGameObject_0;
    private GameObject[] animatedGameObject_1;
    private GameObject parentGameObject;
    [SerializeField]
    private GameObject onLevelDoneLogo;


    public void Start()
    {
        currentSelectedLevel = Database.LevelRelated.selectedLevelFromScene;
        Debug.Log("Level Index (StartingFrom0): " + currentSelectedLevel);
        untouchedColor = currentLevelProgress = 0;

        Database.Functions.LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");
        levelColorChecker = new bool[levelData[currentSelectedLevel].tileData.Length];

        Database.LevelRelated.gridLevelSize = levelData[currentSelectedLevel].gridSize;
        levelColorChecker = new bool[levelData[currentSelectedLevel].gridSize * levelData[currentSelectedLevel].gridSize];

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
                GameObject.Find("Bar (" + untouchedColor).gameObject.SetActive(true);
                levelStatusUIChildren[untouchedColor++].color = x;

                cubeTileChildren[i].material.SetColor("_Color", x);
            }
            else
            {
                levelColorChecker[i - 1] = true;
                cubeTileChildren[i].material.SetColor("_Color", colorPallete.colors[0]);
            }
        }

        // Deactivate StatusBar that are unnecessary
        for (int i = untouchedColor; i < levelStatusUIChildren.Length; i++)
        {
            GameObject.Find("Bar (" + i).gameObject.SetActive(false);
        }

        currentLevelProgress = untouchedColor;
        GameManagerLoader();
        PlayStartAnimation();
    }

    public void  Update()
    {
        #region VariableAssigning
        int touchedPlaneIndex = CheckChildCollide.collidedTileIndex;
        int touchedCubeSideIndex = TouchingCubeArea.touchingSideIndex;

        if(cubePlayer.transform.position.y != 1f)
        {
            touchedPlaneIndex = touchedCubeSideIndex = 0;
        }

        TileColor colorInPlane = levelData[currentSelectedLevel].tileColor[touchedPlaneIndex];
        TileColor colorOnSide = levelData[currentSelectedLevel].cubeSidesColor[touchedCubeSideIndex];
        #endregion


        if(levelData[currentSelectedLevel].tileColor[touchedPlaneIndex] == levelData[currentSelectedLevel].cubeSidesColor[touchedCubeSideIndex])
        {
            if(!levelColorChecker[touchedPlaneIndex])
            {
                Debug.Log(levelData[currentSelectedLevel].tileColor[touchedPlaneIndex] + "s are touching");

                currentLevelProgress--;
                for (int i = 0; i < untouchedColor; i++)
                {
                    if(levelStatusUIChildren[i].color == Database.Functions.ColorEnumToColorUnity(levelData[currentSelectedLevel].tileColor[touchedPlaneIndex], colorPallete))
                    {
                        if(GameObject.Find("Bar (" + i) == null)
                            continue;

                        GameObject.Find("Bar (" + i).gameObject.SetActive(false);
                        break;
                    }

                }

                if(currentLevelProgress <= 0)
                    onLevelDoneLogo.SetActive(true);
                
            }
            levelColorChecker[touchedPlaneIndex] = true;
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

        cubePlayer.transform.position = new Vector3(1.6f, 23f, 1.6f);

        parentGameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        parentGameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        parentGameObject.transform.LeanRotate(new Vector3(0, 0, 0), startAnimationDuration).setEaseInOutElastic();
        parentGameObject.transform.LeanScale(Vector3.one, startAnimationDuration).setEaseOutBounce().setOnComplete(async () =>
        {
            cubePlayer.GetComponent<CubeMovement>().FallingAnimation();
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
            anim.transform.LeanScale(Vector3.one, Mathf.Sqrt(startAnimationDuration) / 2).setEaseOutBounce();
            yield return new WaitForSeconds(0.2f);
        }   
    }
}