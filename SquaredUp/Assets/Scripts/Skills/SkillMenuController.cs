using UnityEngine;

/// <summary>Controls the opening and closing of the skill menu</summary>
public class SkillMenuController : MonoBehaviour
{
    // Name of the skill menu action map in player input
    [SerializeField] private string skillMenuActionMapName = "SkillMenu";

    // Pause events
    public delegate void OpenSkillMenu();
    public static event OpenSkillMenu OpenSkillMenuEvent;
    public delegate void CloseSkillMenu();
    public static event CloseSkillMenu CloseSkillMenuEvent;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.OpenSkillMenuEvent += OnOpenSkillMenu;
        InputEvents.CloseSkillMenuEvent += OnCloseSkillMenu;
        InputEvents.RevertEvent += OnCloseSkillMenu;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.OpenSkillMenuEvent -= OnOpenSkillMenu;
        InputEvents.CloseSkillMenuEvent -= OnCloseSkillMenu;
        InputEvents.RevertEvent -= OnCloseSkillMenu;
    }

    /// <summary>Called when the player wants to open the skill menu. Invokes OpenSkillMenuEvent.</summary>
    private void OnOpenSkillMenu()
    {
        // Swap input maps and call the open event
        InputController.Instance.SwitchInputMap(skillMenuActionMapName);
        OpenSkillMenuEvent?.Invoke();
    }
    /// <summary>Called when the player wants to close the skill menu. Invokes OpenSkillMenuEvent.</summary>
    private void OnCloseSkillMenu()
    {
        // Revert the input map and call the close event
        InputController.Instance.PopInputMap(skillMenuActionMapName);
        CloseSkillMenuEvent?.Invoke();
    }
}
