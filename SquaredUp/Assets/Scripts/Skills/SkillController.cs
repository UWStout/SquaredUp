using UnityEngine;

// Manages the player's skills
public class SkillController : MonoBehaviour
{
    // References to skills
    private ChangeShapeSkill changeShapeSkill = null;
    private ChangeColorSkill changeColorSkill = null;


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
    }

    /// <summary>Uses all the skills in the skill controller</summary>
    /// <param name="shape">Which shape to change into</param>
    /// <param name="color">Which color to turn</param>
    public void UseSkills(ChangeShapeSkill.Shape shape, ChangeColorSkill.ChangeColor color)
    {
        // Color must be called first
        changeColorSkill.Use((int)color);
        // Shape
        changeShapeSkill.Use((int)shape);
    }
}
