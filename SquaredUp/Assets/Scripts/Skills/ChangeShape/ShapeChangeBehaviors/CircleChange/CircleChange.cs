using UnityEngine;

/// <summary>
/// When we change to the circle, allow the player to go through circle only areas.
/// </summary>
[CreateAssetMenu(fileName = "Circle Change Behavior", menuName = "ScriptableObjects/ShapeChange/CircleData")]
public class CircleChange : ShapeChangeBehavior
{
    // Reference to the player's collision object with the circle specific walls
    // Should be initialized from CircleChangeInitializer
    private GameObject playerCircleWallColliderObj = null;
    public GameObject PlayerCircleWallColliderObj { set { playerCircleWallColliderObj = value; } }


    // When we change to the circle
    public override void ChangeTo()
    {
        // Turn off the player's collider
        playerCircleWallColliderObj.SetActive(false);
        // Fade out the tiles
        StartTileFade(0.5f);
    }

    // When we change off the circle
    public override void ChangeFrom()
    {
        // Turn on the player's collider
        playerCircleWallColliderObj.SetActive(true);
        // Fade in the tiles
        StartTileFade(1f);
    }

    /// <summary>Starts fading the tiles.</summary>
    /// <param name="alphaVal">Alpha value to fade towards.</param>
    private void StartTileFade(float alphaVal)
    {
        CircleTilemapSingleton circleTilemap = CircleTilemapSingleton.Instance;
        if (circleTilemap != null)
        {
            circleTilemap.StartTileFade(alphaVal);
        }
    }
}
