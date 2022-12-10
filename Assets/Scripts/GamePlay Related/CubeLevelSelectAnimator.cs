using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLevelSelectAnimator : MonoBehaviour
{
    float animationQueue;
    public Vector3 initialPosition;
    void OnEnable()
    {
        int x = Random.Range(0, 360);
        bool isNumeric = int.TryParse(this.gameObject.name, out int n);

        if (isNumeric)
            animationQueue = n;
        else
            return;

        animationQueue /= 5;

        if(initialPosition == Vector3.zero)
        {
            initialPosition = transform.localPosition;
        }

        transform.localPosition = new Vector3(0, -Screen.height);

        transform.rotation = Quaternion.Euler(0, 0, (float)x);
        this.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0,0,0);

        StartCoroutine(DelayAnimation());
    }

    void OnDisable()
    {
        transform.localPosition = initialPosition;
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
