using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneRunner : MonoBehaviour
{
    public GameObject cubeButton;
    public Transform cubeButtonTargetPos;
    public float cubeFloatingDuration;
    public float fadingDuration;

    public GameObject homeUI;
    private CanvasGroup homeUICanvas;
    public GameObject levelCardSelectUI;
    private CanvasGroup levelCardSelectUICanvas;

    void Awake()
    {
        homeUICanvas = homeUI.GetComponent<CanvasGroup>();
        //homeUICanvas.alpha = 1;

        levelCardSelectUICanvas = levelCardSelectUI.GetComponent<CanvasGroup>();
        //levelCardSelectUICanvas.alpha = 0;
    }


    void Update()
    {
        
    }

    public void onPlayButtonClickedAnimation()
    {
        homeUI.SetActive(true);
            homeUICanvas.alpha = 1f;
            homeUICanvas.LeanAlpha(0, fadingDuration).setOnComplete(async() =>
            {
                homeUI.SetActive(false);
                //homeUICanvas.alpha = 0;

            });
        cubeButton.LeanMoveY(cubeButtonTargetPos.position.y, cubeFloatingDuration).setEaseInOutElastic().setOnComplete(async() =>
        {
            

            levelCardSelectUI.SetActive(true);
            levelCardSelectUICanvas.alpha = 0;
            levelCardSelectUICanvas.LeanAlpha(1, fadingDuration).setOnComplete(async() =>
            {
                levelCardSelectUI.SetActive(true);
                //levelCardSelectUICanvas.alpha = 0;
            }); 
        });

        cubeButton.LeanScale(cubeButton.transform.localScale / 2, cubeFloatingDuration).setEaseInOutBounce();
    }
    public void onReverseButtonClickedAnimation()
    {
         levelCardSelectUI.SetActive(true);
            
            levelCardSelectUICanvas.alpha = 1;
            levelCardSelectUICanvas.LeanAlpha(0, fadingDuration).setOnComplete(async() =>
            {
                levelCardSelectUI.SetActive(false);
                //levelCardSelectUICanvas.alpha = 0;
            }); 
        

           
        cubeButton.LeanMoveY(0, cubeFloatingDuration).setEaseInOutElastic().setOnComplete(async () =>
        {
            homeUI.SetActive(true);
            
            homeUICanvas.alpha = 0;
            homeUICanvas.LeanAlpha(1, fadingDuration).setOnComplete(async() =>
            {
                homeUI.SetActive(true);
                //homeUICanvas.alpha = 1;
            });
            
        });;
        cubeButton.LeanScale(cubeButton.transform.localScale * 2, cubeFloatingDuration).setEaseInOutBounce();
    }
}
