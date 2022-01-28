using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TileData[][] tile = Tile.fill;

    void Awake()
    {
        tile[0][0] = TileData.Empty;
    }
}
