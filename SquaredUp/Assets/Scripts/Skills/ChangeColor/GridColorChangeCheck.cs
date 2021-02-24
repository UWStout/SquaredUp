using UnityEngine;
using UnityEngine.Tilemaps;

// Manages the tilemap colliders to be on/off when told
public class GridColorChangeCheck : MonoBehaviour
{
    // References to the tilemap colliders
    [SerializeField] private TilemapCollider2D greenTileCol = null;
    [SerializeField] private TilemapCollider2D redTileCol = null;
    [SerializeField] private TilemapCollider2D blueTileCol = null;

    /// <summary>Allows the player to walk through the given color type. Default means none</summary>
    public void DisableColorWalls(ChangeColorSkill.ChangeColor color)
    {
        switch (color)
        {
            case ChangeColorSkill.ChangeColor.DEFAULT:
                EnableAllColliders();
                break;
            case ChangeColorSkill.ChangeColor.GREEN:
                DisableOneCollider(greenTileCol);
                break;
            case ChangeColorSkill.ChangeColor.RED:
                DisableOneCollider(redTileCol);
                break;
            case ChangeColorSkill.ChangeColor.BLUE:
                DisableOneCollider(blueTileCol);
                break;
            default:
                break;
        }
    }

    /// <summary>Allows the player to walk through the given tilemap collider</summary>
    private void DisableOneCollider(TilemapCollider2D tileCol)
    {
        EnableAllColliders();
        tileCol.isTrigger = true;
    }
    /// <summary>Makes all of the tilemap colliders solid</summary>
    private void EnableAllColliders()
    {
        greenTileCol.isTrigger = false;
        redTileCol.isTrigger = false;
        blueTileCol.isTrigger = false;
    }
}
