using UnityEngine;

// Manages the player's skills
public class SkillController : MonoBehaviour
{
    // References to skills
    private ChangeShapeSkill changeShapeSkill = null;
    private ChangeColorSkill changeColorSkill = null;
    private ChangeZoomSkill changeZoomSkill = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        changeShapeSkill = FindObjectOfType<ChangeShapeSkill>();
        if (changeShapeSkill == null)
        {
            Debug.LogError("SkillController could not find ChangeShapeSkill");
        }
        changeColorSkill = FindObjectOfType<ChangeColorSkill>();
        if (changeColorSkill == null)
        {
            Debug.LogError("SkillController could not find ChangeColorSkill");
        }
        changeZoomSkill = FindObjectOfType<ChangeZoomSkill>();
        if (changeZoomSkill == null)
        {
            Debug.LogError("SkillController could not find ChangeZoomSkill");
        }
    }

    /// <summary>Uses all the skills in the skill controller</summary>
    /// <param name="shape">Which shape to change into</param>
    /// <param name="color">Which color to turn</param>
    /// <param name="zoom">Which position to zoom to</param>
    public void UseSkills(ChangeShapeSkill.Shape shape, ChangeColorSkill.ChangeColor color,
        ChangeZoomSkill.ZoomLevel zoom)
    {
        // Color must be called first
        changeColorSkill.Use((int)color);
        // Shape
        changeShapeSkill.Use((int)shape);
        // Zoom
        changeZoomSkill.Use((int)zoom);
    }
}
