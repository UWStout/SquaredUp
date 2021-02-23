using UnityEngine;

// TODO This will be written when the HUD is implemented
public class SkillController : MonoBehaviour
{
    [SerializeField]
    private Skill currentSkill;

    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.UseAbilityEvent += OnUseAbility;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.UseAbilityEvent -= OnUseAbility;
    }

    private void OnUseAbility()
    {
        currentSkill.Use();
    }
}
