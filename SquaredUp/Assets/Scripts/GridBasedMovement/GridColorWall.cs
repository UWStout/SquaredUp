using UnityEngine;

/// <summary>
/// Grid collider for color walls.
/// </summary>
public class GridColorWall : GridHittable
{
    [SerializeField] private Material colorToPassThroughWall = null;


    public override bool Hit(GridHit hit)
    {
        // Don't let things that are not the player through
        if (!hit.moverObj.CompareTag("Player"))
        {
            return false;
        }

        // Only let the player through if the player's current color is the needed color
        return ChangeColorSkill.Instance.GetTargetColor() == colorToPassThroughWall.color;
    }
}
