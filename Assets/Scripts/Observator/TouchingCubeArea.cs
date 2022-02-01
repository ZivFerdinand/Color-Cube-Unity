using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//3
enum SideNames
{
    Null,
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
            case "Side1":
            {
                touchingSide = SideNames.Down;
                break;
            }
            case "Side2":
            {
                touchingSide = SideNames.Up;
                break;
            }
            case "Side3":
            {
                touchingSide = SideNames.Left;
                break;
            }
            case "Side4":
            {
                touchingSide = SideNames.Front;
                break;
            }
            case "Side5":
            {
                touchingSide = SideNames.Right;
                break;
            }
            case "Side6":
            {
                touchingSide = SideNames.Back;
                break;
            }
        }
    }
}
