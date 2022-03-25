using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLevelSelectAnimator : MonoBehaviour
{
    float animationQueue;
    Vector3 initialPosition;
    void OnEnable()
    {
        int n;
        bool isNumeric = int.TryParse(this.gameObject.name, out n);

        if(isNumeric)
            animationQueue = int.Parse(this.gameObject.name);

        animationQueue /= 5;

        initialPosition = transform.localPosition;
        transform.localPosition = new Vector3(0, -Screen.height);
    }

    void Start()
    {
        StartCoroutine(DelayAnimation());
    }

    private IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(animationQueue);

        this.gameObject.LeanMoveLocal(new Vector3(initialPosition.x, initialPosition.y , 0f), 1f).setEaseInOutExpo();
    }

    public IEnumerator DelayExitAnimation()
    {
        yield return new WaitForSeconds(animationQueue);

        this.gameObject.LeanMoveLocal(new Vector3(0, -Screen.height, 0), 1f).setEaseInOutExpo();
    }
}
