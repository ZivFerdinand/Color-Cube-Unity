using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneRunner : MonoBehaviour
{
    public GameObject cubeButton;
    public Transform cubeButtonTargetPos;
    public float cubeFloatingDuration;

    public GameObject homeUI;
    public GameObject levelCardSelectUI;

    void Start()
    {
    }


    void Update()
    {
        
    }

    public void onPlayButtonClickedAnimation()
    {
        cubeButton.LeanMoveY(cubeButtonTargetPos.position.y, cubeFloatingDuration).setEaseInOutElastic();
        cubeButton.LeanScale(cubeButton.transform.localScale / 2, cubeFloatingDuration).setEaseInOutBounce().setOnComplete(async () =>
        {
            homeUI.SetActive(false);
            levelCardSelectUI.SetActive(true);  
        });
    }
    public void onReverseButtonClickedAnimation()
    {
            levelCardSelectUI.SetActive(false);
            homeUI.SetActive(true);
        cubeButton.LeanMoveY(0, cubeFloatingDuration).setEaseInOutElastic();
        cubeButton.LeanScale(cubeButton.transform.localScale * 2, cubeFloatingDuration).setEaseInOutBounce();
    }
}
