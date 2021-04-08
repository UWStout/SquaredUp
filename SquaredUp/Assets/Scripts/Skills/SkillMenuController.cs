using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Controls the opening and closing of the skill menu</summary>
public class SkillMenuController : MonoBehaviour
{
    // Input References
    // Reference to the player input
    [SerializeField] private PlayerInput playerInputRef = null;
    // Name of the skill menu action map in player input
    [SerializeField] private string skillMenuActionMapName = "SkillMenu";
    // Name of the player action map in player input
    [SerializeField] private string playerActionMapName = "Player";

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

    /// <summary>Called when the player wants to pause. Invokes GamePauseEvent.</summary>
    private void OnOpenSkillMenu()
    {
        // Pause
        playerInputRef.SwitchCurrentActionMap(skillMenuActionMapName);
        OpenSkillMenuEvent?.Invoke();
    }
    /// <summary>Called when the player wants to unpause. Invokes GameUnpauseEvent.</summary>
    private void OnCloseSkillMenu()
    {
        // Unpause
        playerInputRef.SwitchCurrentActionMap(playerActionMapName);
        CloseSkillMenuEvent?.Invoke();
    }
}
