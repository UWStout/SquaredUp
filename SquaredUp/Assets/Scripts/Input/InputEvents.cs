using UnityEngine;
using UnityEngine.InputSystem;

// Handles event calling for input events
[RequireComponent(typeof(PlayerInput))]
public class InputEvents : MonoBehaviour
{
    // Device Lost
    public delegate void DeviceLost();
    public static event DeviceLost DeviceLostEvent;
    private void OnDeviceLost()
    {
        DeviceLostEvent?.Invoke();
    }

    // Device Regained
    public delegate void DeviceRegained();
    public static event DeviceRegained DeviceRegainedEvent;
    private void OnDeviceRegained()
    {
        DeviceRegainedEvent?.Invoke();
    }

    // Controls Changed
    public delegate void ControlsChanged();
    public static event ControlsChanged ControlsChangedEvent;
    private void OnControlsChanged()
    {
        ControlsChangedEvent?.Invoke();
    }

    // Movement
    public delegate void Movement(Vector2 rawMovement);
    public static event Movement MovementEvent;
    private void OnMovement(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnMovement" + rawInpVec);
        MovementEvent?.Invoke(rawInpVec);
    }

    // Interact
    public delegate void Interact();
    public static event Interact InteractEvent;
    private void OnInteract()
    {
        //Debug.Log("OnInteract");
        InteractEvent?.Invoke();
    }

    // UseAbility
    public delegate void UseAbility();
    public static event UseAbility UseAbilityEvent;
    private void OnUseAbility()
    {
        //Debug.Log("OnUseAbility");
        UseAbilityEvent?.Invoke();
    }

    // AdvanceDialogue
    public delegate void AdvanceDialogue();
    public static event AdvanceDialogue AdvanceDialogueEvent;
    private void OnAdvanceDialogue()
    {
        //Debug.Log("OnAdvanceDialogue");
        AdvanceDialogueEvent?.Invoke();
    }

    // OpenSkillMenu
    public delegate void OpenSkillMenu();
    public static event OpenSkillMenu OpenSkillMenuEvent;
    private void OnOpenSkillMenu()
    {
        //Debug.Log("OnOpenSkillMenu");
        OpenSkillMenuEvent?.Invoke();
    }

    // CloseSkillMenu
    public delegate void CloseSkillMenu();
    public static event CloseSkillMenu CloseSkillMenuEvent;
    private void OnCloseSkillMenu()
    {
        //Debug.Log("OnCloseSkillMenu");
        CloseSkillMenuEvent?.Invoke();
    }

    // PauseGame
    public delegate void PauseGame();
    public static event PauseGame PauseGameEvent;
    private void OnPauseGame()
    {
        //Debug.Log("OnPauseGame");
        PauseGameEvent?.Invoke();
    }

    public delegate void UnpauseGame();
    public static event UnpauseGame UnpauseGameEvent;
    private void OnUnpauseGame()
    {
        //Debug.Log("OnPauseGame");
        UnpauseGameEvent?.Invoke();
    }


    // MainAxis
    public delegate void MainAxis(Vector2 rawMainAxis);
    public static event MainAxis MainAxisEvent;
    private void OnMainAxis(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnMainAxis" + rawInpVec);
        MainAxisEvent?.Invoke(rawInpVec);
    }

    // HackerAxis
    public delegate void HackerAxis(Vector2 rawHackerAxis);
    public static event HackerAxis HackerAxisEvent;
    private void OnHackerAxis(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnHackerAxis" + rawInpVec);
        HackerAxisEvent?.Invoke(rawInpVec);
    }

    // Zoom
    public delegate void Zoom();
    public static event Zoom ZoomEvent;
    private void OnZoom()
    {
        //Debug.Log("OnZoom" + rawInpVec);
        ZoomEvent?.Invoke();
    }

    // ShapeUpdate
    public delegate void ShapeUpdate();
    public static event ShapeUpdate ShapeUpdateEvent;
    private void OnShapeUpdate()
    {
        //Debug.Log("OnShapeUpdate" + rawInpVec);
        ShapeUpdateEvent?.Invoke();
    }

    // Revert
    public delegate void Revert();
    public static event Revert RevertEvent;
    private void OnRevert()
    {
        //Debug.Log("OnRevert" + rawInpVec);
        RevertEvent?.Invoke();
    }
}
