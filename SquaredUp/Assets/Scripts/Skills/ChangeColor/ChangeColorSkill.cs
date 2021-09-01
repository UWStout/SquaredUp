using System.Collections;
using UnityEngine;

/// <summary>Skill to change the player's color</summary>
public class ChangeColorSkill : SkillBase<ColorData>
{
    // Singleton
    private static ChangeColorSkill instance;
    public static ChangeColorSkill Instance { get { return instance; } }


    //SFX for color transformation
    public AudioSource transformColor;
    // References
    // Reference to the mesh renderer whose material will be changed
    [SerializeField] private SkinnedMeshRenderer playerMeshRendRef = null;
    // Reference to the sprite whose color will be changed as well
    [SerializeField] private SpriteRenderer playerPointerSprRef = null;
    // Reference to the color check script
    [SerializeField] private PlayerInColorCheck playerColorCheckRef = null;
    // SFX for failing transformation
    [SerializeField] private AudioSource failedTransformSound = null;

    // Coroutine variables for how fast to change the color and when we are close enough
    [SerializeField] [Min(0.0001f)] private float changeSpeed = 1f;
    // If the coroutine is finished
    private bool changeColorCoroutFin = true;
    // Refrence to the coroutine running
    private Coroutine changeColorCorout = null;

    // Target material to change color to match
    private Material targetMat = null;

    // References to the gameobjects on the player that have the wall colliders on them
    // The order of the walls MUST match the enum
    [SerializeField] private GameObject[] coloredWallColliders = new GameObject[0];


    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple ChangeColorSkills");
            Destroy(this.gameObject);
        }
    }


    /// <summary>Changes the player's material to the material with color corresponding to the given index.
    /// Index matches what is specified in the editor. If index is unknown, consider using Use(ColorData) instead.</summary>
    public override void Use(int stateIndex)
    {
        if (!playerColorCheckRef.IsInWall)
        {
            if (UpdateCurrentState(stateIndex))
            {
                StartColorChange(stateIndex);
                transformColor.Play();
            }
        }
        else
        {
            failedTransformSound.Play();
        }
    }

    /// <summary>Returns the index of which wall color the player can walk through currently. If the player
    /// cannot walk through any colored walls, returns -1.</summary>
    public int GetPassableWallColorIndex()
    {
        for (int i = 0; i < coloredWallColliders.Length; ++i)
        {
            GameObject obj = coloredWallColliders[i];
            if (!obj.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Gets the color of the target material.
    /// </summary>
    /// <returns>Color of the target material.</returns>
    public Color GetTargetColor()
    {
        if (targetMat == null)
        {
            return Color.white;
        }

        return targetMat.color;
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

    /// <summary>Starts the coroutine to smoothly change color or if the coroutine is already active, updates the target material</summary>
    /// <param name="stateIndex">Index of the state/material to change the player to/param>
    private void StartColorChange(int stateIndex)
    {
        targetMat = SkillData.GetData(stateIndex).Material;
        // If a coroutine is currently going, stop it and start a new one
        if (!changeColorCoroutFin)
        {
            StopCoroutine(changeColorCorout);
        }
        changeColorCorout = StartCoroutine(ColorChangeCoroutine(stateIndex));
    }

    /// <summary>Coroutine to smoothly change the player's material/color to the target material</summary>
    private IEnumerator ColorChangeCoroutine(int stateIndex)
    {
        changeColorCoroutFin = false;

        // Create a temporary material that begins as a copy of the player's current material
        Material dupMat = new Material(playerMeshRendRef.material.shader);
        dupMat.CopyPropertiesFromMaterial(playerMeshRendRef.material);
        // Swap the copy for the original
        playerMeshRendRef.material = dupMat;
        // Start color
        Color startCol = dupMat.color;

        float t = 0;
        // Start changing the colors
        while (t < 1)
        {
            // Lerp albedo color
            dupMat.color = Color.Lerp(startCol, targetMat.color, t);
            playerPointerSprRef.color = dupMat.color;

            // Step
            t += changeSpeed * Time.deltaTime;

            yield return null;
        }
        playerMeshRendRef.material = targetMat;
        playerPointerSprRef.color = targetMat.color;

        // Do away with the temp mat
        Destroy(dupMat);
        // Let the player pass through the corresponding colored area
        AllowColorPassage(stateIndex);
        // Mark coroutine as finished
        changeColorCoroutFin = true;

        yield return null;
    }
}
