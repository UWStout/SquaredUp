using UnityEngine;

// Skill to change the player's color
public class ChangeColorSkill : Skill
{
    // Supported colors to change to
    public enum ChangeColor { DEFAULT, GREEN, RED, BLUE };

    // Reference to the mesh renderer whose material will be changed
    [SerializeField] private MeshRenderer playerMeshRendRef = null;

    // Materials for the color changing
    [SerializeField] private Material defaultMat = null;
    [SerializeField] private Material greenMat = null;
    [SerializeField] private Material redMat = null;
    [SerializeField] private Material blueMat = null;

    // Coroutine variables for how fast to change the color and when we are close enough
    [SerializeField] private float changeSpeed = 0.03f;
    [SerializeField] private float closeEnoughVal = 0.001f;

    // Reference to the player in color script that will let us know if we can change color
    private PlayerInColorCheck playColCheckRef = null;

    // References to the gameobjects that have the wall colliders on them
    [SerializeField] private GameObject greenWall = null;
    [SerializeField] private GameObject redWall = null;
    [SerializeField] private GameObject blueWall = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        playColCheckRef = FindObjectOfType<PlayerInColorCheck>();
        if (playerMeshRendRef == null)
        {
            Debug.LogError("ChangeColorSkill could not find PlayerInColorCheck");
        }
    }

    /// <summary>Changes the player's material to the material with the correct color</summary>
    /// <param name="enumVal">Should be a value of the enum ChangeColor</param>
    public override void Use(int enumVal)
    {
        ChangeColor col = (ChangeColor)enumVal;
        if (!playColCheckRef.IsInWall)
        {
            playerMeshRendRef.material = GetMaterial(col);
            AllowColorPassage(col);
        }
        else
        {
            Debug.Log("Player cannot change to " + col + " while in non-" + col.ToString().ToLower() + " area");
        }
    }

    /// <summary>Converts the current ChangeColor enum to its associated material</summary>
    private Material GetMaterial(ChangeColor changeToColor)
    {
        switch (changeToColor)
        {
            case ChangeColor.DEFAULT:
                return defaultMat;
            case ChangeColor.GREEN:
                return greenMat;
            case ChangeColor.RED:
                return redMat;
            case ChangeColor.BLUE:
                return blueMat;
            default:
                Debug.LogError("Unknown ChangeColor " + changeToColor + " in ChangeColorSkill");
                return defaultMat;
        }
    }

    /// <summary>Turns off the collider that inhbits passage through the given color</summary>
    private void AllowColorPassage(ChangeColor color)
    {
        DenyAllColorPassage();
        switch (color)
        {
            case ChangeColor.DEFAULT:
                break;
            case ChangeColor.GREEN:
                greenWall.SetActive(false);
                break;
            case ChangeColor.RED:
                redWall.SetActive(false);
                break;
            case ChangeColor.BLUE:
                blueWall.SetActive(false);
                break;
            default:
                Debug.LogError("Unknown ChangeColor " + color + " in ChangeColorSkill");
                break;
        }
    }

    /// <summary>Turn off all colors</summary>
    private void DenyAllColorPassage()
    {
        greenWall.SetActive(true);
        redWall.SetActive(true);
        blueWall.SetActive(true);
    }
}
