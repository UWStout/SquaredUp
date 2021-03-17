using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Controls the pausing and unpausing of the game</summary>
public class PauseController : MonoBehaviour
{
    // Input References
    // Reference to the player input
    [SerializeField] private PlayerInput playerInputRef = null;
    // Name of the pause action map in player input
    [SerializeField] private string pauseActionMapName = "Pause";
    // Name of the player action map in player input
    [SerializeField] private string playerActionMapName = "Player";

    // Pause events
    public delegate void GamePause();
    public static event GamePause GamePauseEvent;
    public delegate void GameUnpause();
    public static event GameUnpause GameUnpauseEvent;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.PauseEvent += OnPause;
        InputEvents.UnpauseEvent += OnUnpause;
        InputEvents.RevertEvent += OnUnpause;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.PauseEvent -= OnPause;
        InputEvents.UnpauseEvent -= OnUnpause;
        InputEvents.RevertEvent -= OnUnpause;
    }

    /// <summary>Called when the player wants to pause. Invokes GamePauseEvent.</summary>
    private void OnPause()
    {
        // Pause
        playerInputRef.SwitchCurrentActionMap(pauseActionMapName);
        GamePauseEvent?.Invoke();
    }
    /// <summary>Called when the player wants to unpause. Invokes GameUnpauseEvent.</summary>
    private void OnUnpause()
    {
        // Unpause
        playerInputRef.SwitchCurrentActionMap(playerActionMapName);
        GameUnpauseEvent?.Invoke();
    }
}
