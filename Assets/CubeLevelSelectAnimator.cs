using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLevelSelectAnimator : MonoBehaviour
{
    float animationQueue;
    Vector3 initialPosition;
    void OnEnable()
    {
        float x = Random.Range(0, 360f);
        int n;
        bool isNumeric = int.TryParse(this.gameObject.name, out n);

        if(isNumeric)
            animationQueue = int.Parse(this.gameObject.name);

        animationQueue /= 5;

        initialPosition = transform.localPosition;
        transform.localPosition = new Vector3(0, -Screen.height);
        transform.rotation = Quaternion.Euler(0, 0, x);
        transform.GetComponentInChildren<Transform>().transform.rotation = Quaternion.Euler(0,0,360f-x);
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
