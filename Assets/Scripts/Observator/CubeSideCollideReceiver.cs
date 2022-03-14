using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSideCollideReceiver : MonoBehaviour
{

    public static int touchingSideIndex;
    

    private void OnTriggerEnter(Collider collision)
    {
        switch(collision.gameObject.name)
        {
            case "Side0":
            {
                touchingSideIndex = 0;
                break;
            }
            case "Side1":
            {
                touchingSideIndex = 1;
                break;
            }
            case "Side2":
            {
                touchingSideIndex = 2;
                break;
            }
            case "Side3":
            {
                touchingSideIndex = 3;
                break;
            }
            case "Side4":
            {
                touchingSideIndex = 4;
                break;
            }
            case "Side5":
            {
                touchingSideIndex = 5;
                break;
            }
        }
    }
}
