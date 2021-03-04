using UnityEngine;

/// <summary>Skill to change the player's color</summary>
public class ChangeColorSkill : SkillBase<ColorData>
{
    // Reference to the mesh renderer whose material will be changed
    [SerializeField] private MeshRenderer playerMeshRendRef = null;

    // Coroutine variables for how fast to change the color and when we are close enough
    [SerializeField] private float changeSpeed = 0.03f;
    [SerializeField] private float closeEnoughVal = 0.001f;

    // References to the gameobjects on the player that have the wall colliders on them
    // The order of the walls MUST match the enum
    [SerializeField] private GameObject[] coloredWallColliders = new GameObject[0];

    /// <summary>Changes the player's material to the material with color corresponding to the given index.
    /// Index matches what is specified in the editor. If index is unknown, consider using Use(ColorData) instead.</summary>
    public override void Use(int stateIndex)
    {
        if (!PlayerInColorCheck.Instance.IsInWall)
        {
            playerMeshRendRef.material = SkillData.GetData(stateIndex).Material;
            AllowColorPassage(stateIndex);
        }
        else
        {
            Debug.Log("Player cannot change to " + GetStateName(stateIndex) + " here");
        }
    }

    /// <summary>Turns off the collider that inhbits passage through the given color</summary>
    /// <param name="index">Index of the color. 0 is default</param>
    private void AllowColorPassage(int index)
    {
        DenyAllColorPassage();
        // Subtract 1 because we don't have a collider for default color
        int colorIndex = index - 1;
        if (colorIndex >= 0)
        {
            coloredWallColliders[colorIndex].SetActive(false);
        }
    }

    /// <summary>Turn off all colors</summary>
    private void DenyAllColorPassage()
    {
        foreach (GameObject obj in coloredWallColliders)
        {
            obj.SetActive(true);
        }
    }
}
