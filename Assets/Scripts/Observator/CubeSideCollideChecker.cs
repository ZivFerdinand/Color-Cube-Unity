using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSideCollideChecker : MonoBehaviour
{

    public static int collidedTileIndex;


    private void OnTriggerEnter(Collider collision) 
    {
        int n;
        bool isNumeric = int.TryParse(collision.name, out n);

        if(isNumeric)
            collidedTileIndex = int.Parse(collision.name);
    }
}
