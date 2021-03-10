using UnityEngine;

/// <summary>Manages the player's skills.
/// Skills must each be set up to their own gameobjects and made children of the gameobject
/// this script is attached to.</summary>
public class HUDSkillController : SkillControllerParent
{
    // Singleton
    private static HUDSkillController instance = null;
    public static HUDSkillController Instance { get { return instance; } }

    // Called 0th
    // Set references
    protected override void Awake()
    {
        base.Awake();

        // Set up singleton
        if (instance == null) { instance = this; }
        else
        {
            Debug.LogError("Cannot have multiple SkillControllers in a scene");
            Destroy(this);
        }
    }
}
