using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManagerLevelEditor : MonoBehaviour
{
    private float startAnimationDuration;
    private List<LevelScriptableObject> levelData = new List<LevelScriptableObject>();

    [SerializeField] 
    private ColorMap colorPallete;
    


    private int levelTotal;
    [SerializeField] private int currentSelectedLevel;

    private MeshRenderer[] cubeTileChildren;
    private MeshRenderer[] cubeSideChildren;


    public GameObject cubeTile;
    public GameObject cubeSides;
    public GameObject cubePlayer;
    public TextMeshProUGUI selectedLevelText;

    private GameObject[] animatedGameObject_0;
    private GameObject[] animatedGameObject_1;
    private GameObject parentGameObject;



    void Start()
    {
        startAnimationDuration = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AnimationManager>().startingAnimationDuration;

        selectedLevelText.text = "Selected Level: " + currentSelectedLevel;
        Debug.Log("Level Index (StartingFrom0): " + currentSelectedLevel);

        Database.Functions.LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");

        Database.LevelRelated.gridLevelSize = levelData[currentSelectedLevel].gridSize;
        
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

                cubeTileChildren[i].material.SetColor("_Color", x);
            }
            else
            {
                cubeTileChildren[i].material.SetColor("_Color", colorPallete.colors[0]);
            }
        }


        GameManagerLoader();
        PlayStartAnimation();
    }

    void Update()
    {
        Database.Functions.LoadGameData<LevelScriptableObject>(ref levelTotal, levelData, "Level SO(s)");

        Database.LevelRelated.gridLevelSize = levelData[currentSelectedLevel].gridSize;

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

                cubeTileChildren[i].material.SetColor("_Color", x);
            }
            else
            {
                cubeTileChildren[i].material.SetColor("_Color", colorPallete.colors[0]);
            }
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