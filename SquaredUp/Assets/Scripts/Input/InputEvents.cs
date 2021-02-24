﻿using UnityEngine;
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

    // Pause
    public delegate void Pause();
    public static event Pause PauseEvent;
    private void OnPause()
    {
        //Debug.Log("OnPause");
        PauseEvent?.Invoke();
    }

    // Unpause
    public delegate void Unpause();
    public static event Unpause UnpauseEvent;
    private void OnUnpause()
    {
        //Debug.Log("OnUnpause");
        UnpauseEvent?.Invoke();
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
}
