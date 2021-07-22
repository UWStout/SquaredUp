using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles event calling for input events
[RequireComponent(typeof(PlayerInput))]
public class InputEvents : MonoBehaviour
{
    // Device Lost
    public static event Action DeviceLostEvent;
    private void OnDeviceLost()
    {
        DeviceLostEvent?.Invoke();
    }

    // Device Regained
    public static event Action DeviceRegainedEvent;
    private void OnDeviceRegained()
    {
        DeviceRegainedEvent?.Invoke();
    }

    // Controls Changed
    public static event Action ControlsChangedEvent;
    private void OnControlsChanged()
    {
        ControlsChangedEvent?.Invoke();
    }

    // Movement
    public static event Action<Vector2> MovementEvent;
    private void OnMovement(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnMovement" + rawInpVec);
        MovementEvent?.Invoke(rawInpVec);
    }

    // SlowWalk
    public static event Action<bool> SlowWalkEvent;
    private void OnSlowWalk(InputValue value)
    {
        //Debug.Log("OnSlowWalk");
        SlowWalkEvent?.Invoke(value.isPressed);
    }

    // Interact
    public static event Action InteractEvent;
    private void OnInteract()
    {
        //Debug.Log("OnInteract");
        InteractEvent?.Invoke();
    }

    // UseAbility
    public static event Action UseAbilityEvent;
    private void OnUseAbility()
    {
        //Debug.Log("OnUseAbility");
        UseAbilityEvent?.Invoke();
    }

    // AdvanceDialogue
    public static event Action AdvanceDialogueEvent;
    private void OnAdvanceDialogue()
    {
        //Debug.Log("OnAdvanceDialogue");
        AdvanceDialogueEvent?.Invoke();
    }

    // OpenSkillMenu
    public static event Action OpenSkillMenuEvent;
    private void OnOpenSkillMenu()
    {
        //Debug.Log("OnOpenSkillMenu");
        OpenSkillMenuEvent?.Invoke();
    }

    // CloseSkillMenu
    public static event Action CloseSkillMenuEvent;
    private void OnCloseSkillMenu()
    {
        //Debug.Log("OnCloseSkillMenu");
        CloseSkillMenuEvent?.Invoke();
    }

    // PauseGame
    public static event Action PauseGameEvent;
    private void OnPauseGame()
    {
        //Debug.Log("OnPauseGame");
        PauseGameEvent?.Invoke();
    }

    // UnpauseGame
    public static event Action UnpauseGameEvent;
    private void OnUnpauseGame()
    {
        //Debug.Log("OnPauseGame");
        UnpauseGameEvent?.Invoke();
    }


    // MainAxis
    public static event Action<Vector2> MainAxisEvent;
    private void OnMainAxis(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnMainAxis" + rawInpVec);
        MainAxisEvent?.Invoke(rawInpVec);
    }

    // HackerAxis
    public static event Action<Vector2> HackerAxisEvent;
    private void OnHackerAxis(InputValue value)
    {
        Vector2 rawInpVec = value.Get<Vector2>();
        //Debug.Log("OnHackerAxis" + rawInpVec);
        HackerAxisEvent?.Invoke(rawInpVec);
    }

    // Zoom
    public static event Action ZoomEvent;
    private void OnZoom()
    {
        //Debug.Log("OnZoom" + rawInpVec);
        ZoomEvent?.Invoke();
    }

    // ShapeUpdate
    public static event Action ShapeUpdateEvent;
    private void OnShapeUpdate()
    {
        //Debug.Log("OnShapeUpdate" + rawInpVec);
        ShapeUpdateEvent?.Invoke();
    }

    // Revert
    public static event Action RevertEvent;
    private void OnRevert()
    {
        //Debug.Log("OnRevert" + rawInpVec);
        RevertEvent?.Invoke();
    }

    // HackerTeleport
    public static event Action<int> HackerTeleportEvent;
    private void OnHackerTeleport(InputValue value)
    {
        //Debug.Log("OnHackerTeleport" + rawInpVec);
        HackerTeleportEvent?.Invoke(Mathf.RoundToInt(value.Get<float>()));
    }

    // Sprint
    public static event Action<bool> SprintEvent;
    private void OnSprint(InputValue value)
    {
        //Debug.Log("OnSprint" + rawInpVec);
        SprintEvent?.Invoke(value.isPressed);
    }
}
