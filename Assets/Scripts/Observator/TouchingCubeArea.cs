using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//3
enum SideNames
{
    Down,
    Up,
    Left,
    Front,  
    Right,
    Back
};

public class TouchingCubeArea : MonoBehaviour
{
    SideNames touchingSide;
    public static int touchingSideIndex;

    private string colliderName;
    void Awake()
    {
        colliderName = "Side1";
    }

    
    void Update()
    {
        //Debug.Log(touchingSide + " (" + colliderName + ")");
    }
    private void OnTriggerEnter(Collider collision)
    {
        colliderName = collision.gameObject.name;
        switch(collision.gameObject.name)
        {
            case "Side0":
            {
                touchingSide = SideNames.Down;
                break;
            }
            case "Side1":
            {
                touchingSide = SideNames.Up;
                break;
            }
            case "Side2":
            {
                touchingSide = SideNames.Left;
                break;
            }
            case "Side3":
            {
                touchingSide = SideNames.Front;
                break;
            }
            case "Side4":
            {
                touchingSide = SideNames.Right;
                break;
            }
            case "Side5":
            {
                touchingSide = SideNames.Back;
                break;
            }
        }

        touchingSideIndex = (int)touchingSide;
    }
}
