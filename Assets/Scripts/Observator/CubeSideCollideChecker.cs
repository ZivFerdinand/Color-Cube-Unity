using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSideCollideChecker : MonoBehaviour
{

    public static int collidedTileIndex;


    private void OnTriggerEnter(Collider collision) 
    {
        bool isNumeric = int.TryParse(collision.name, out int n);

        if (isNumeric)
            collidedTileIndex = n;
    }
}
