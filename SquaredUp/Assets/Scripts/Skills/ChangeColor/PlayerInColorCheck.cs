﻿using System.Collections.Generic;
using UnityEngine;

///<summary>Checks if the player is inside a wall.
///
/// Assumptions:
///  This gameobject is on the WallCheck layer and
///  the WallCheck layer only interacts with walls
///</summary>
public class PlayerInColorCheck : MonoBehaviour
{
    // If the player is in a colored wall
    public bool isInWall { get; private set; }

    // The amount of walls the player is in
    private int amountWallsIn = 0;


    // When player enters a wall, player is now in a wall
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ++amountWallsIn;
        isInWall = true;
    }
    // When player exits in a wall, player is no longer in a wall
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the amount of walls is now zero, we are no longer in any walls
        --amountWallsIn;
        if (amountWallsIn <= 0)
        {
            isInWall = false;
            amountWallsIn = 0;
        }
    }
}
