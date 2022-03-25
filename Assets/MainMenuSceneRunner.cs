using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainMenuSceneRunner : MonoBehaviour
{
    public GameObject cubeButton;
    public Transform cubeButtonTargetPos;
    public float cubeFloatingDuration;
    public float fadingDuration;


    public GameObject cubeForLevelSelectParent;
    private CubeLevelSelectAnimator[] cubeForLevelSelect;


    public GameObject homeUI;
    public GameObject particleCube;
    private CanvasGroup homeUICanvas;
    public GameObject levelCardSelectUI;
    private CanvasGroup levelCardSelectUICanvas;
    public GameObject levelSelectUI;

    void Awake()
    {
        cubeForLevelSelect = cubeForLevelSelectParent.GetComponentsInChildren<CubeLevelSelectAnimator>();


        homeUICanvas = homeUI.GetComponent<CanvasGroup>();
        levelCardSelectUICanvas = levelCardSelectUI.GetComponent<CanvasGroup>();
    }


    void Update()
    {
        
    }

    public void onPlayButtonClickedAnimation()
    {
        //HOMEUI FADE
        homeUI.SetActive(true);
        homeUICanvas.alpha = 1f;
        homeUICanvas.LeanAlpha(0, fadingDuration).setOnComplete(async() =>
        {
            homeUI.SetActive(false);
        });

        cubeButton.LeanMoveY(cubeButtonTargetPos.position.y, cubeFloatingDuration).setEaseInOutElastic().setOnComplete(async() =>
        {
            //CARDLEVEL FADE
            levelCardSelectUI.SetActive(true);
            levelCardSelectUICanvas.alpha = 0;
            levelCardSelectUICanvas.LeanAlpha(1, fadingDuration);
        });

        cubeButton.LeanScale(cubeButton.transform.localScale / 2, cubeFloatingDuration).setEaseInOutBounce();
    }
    public async void onReverseButtonClickedAnimation()
    {
        #region ToMainScreen
        if (levelCardSelectUICanvas.alpha == 1)
        {
            //CARDLEVEL FADE
            levelCardSelectUI.SetActive(true);
            levelCardSelectUICanvas.alpha = 1;
            levelCardSelectUICanvas.LeanAlpha(0, fadingDuration).setOnComplete(async () =>
            {
                levelCardSelectUI.SetActive(false);
            });


            cubeButton.LeanMoveY(0, cubeFloatingDuration).setEaseInOutElastic().setOnComplete(async () =>
            {
                //HOMEUI FADE
                homeUI.SetActive(true);
                homeUICanvas.alpha = 0;
                homeUICanvas.LeanAlpha(1, fadingDuration);

            }); ;
            cubeButton.LeanScale(cubeButton.transform.localScale * 2, cubeFloatingDuration).setEaseInOutBounce();
        }
        #endregion

        #region ToCardSelect
        else
        {
            StartCoroutine(FadeWaiter());
        }
        #endregion
    }

    public void onMapSelectedAnimation()
    {
        particleCube.SetActive(false);
        //CARDLEVEL FADE
        levelCardSelectUI.SetActive(true);
        levelCardSelectUICanvas.alpha = 1;
        levelCardSelectUICanvas.LeanAlpha(0, fadingDuration).setOnComplete(async() =>
        {
            levelSelectUI.SetActive(true);
            levelCardSelectUI.SetActive(false);
        });
        
    }

    IEnumerator FadeWaiter()
    {
        foreach(CubeLevelSelectAnimator a in cubeForLevelSelect)
        {
            StartCoroutine(a.DelayExitAnimation());
        }
        yield return new WaitForSeconds(1f);
        particleCube.SetActive(true);

        //CARDLEVEL FADE
        levelCardSelectUI.SetActive(true);
        levelCardSelectUICanvas.alpha = 0;
        levelCardSelectUICanvas.LeanAlpha(1, fadingDuration);
    }
}
