using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Transform cubeTransform;
    void Awake()
    {
        touchingSide = SideNames.Back;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("a");
        if(!CubeMovement.isRolling)
        {
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

    void Update()
    {
        
        Debug.Log(touchingSide);
    }
}
