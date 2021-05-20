using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private inputReferences[] inputReferenceArray;
    [SerializeField] private TMP_Text[] bindingDisplayNameText;
    private InputActionReference inputReference_;
    private int control;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private string[] move = { "Up", "Down", "Left", "Right" };

    private void Awake()
    {
        
    }

    public void StartRebindingComposite(int control_) 
    {
        /*
        control = control_;
        InputController.Instance.SwitchInputMap("EndGame");

        rebindOperation = inputReferenceArray[control].inputActions[0].action.PerformInteractiveRebinding()
            .WithTargetBinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(.1f)
            .OnComplete(operation => RebindCompositeComplete())
            .Start();
        */
    }

    private void RebindCompositeComplete()
    {
        /*
        for (int i = 1; i < inputReferenceArray[control].inputActions.Length; i++)
        {
            inputReferenceArray[control].inputActions[i].action.ApplyBindingOverride(0, inputReferenceArray[control].inputActions[0].action.bindings[0].effectivePath);
        } 
        bindingDisplayNameText[control].text = InputControlPath.ToHumanReadableString(
            inputReferenceArray[control].inputActions[0].action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        */
        
        rebindOperation.Dispose();
        InputController.Instance.SwitchInputMap("PauseGame");

    }

    public void StartRebinding(int control_)
    {
        control = control_;
        InputController.Instance.SwitchInputMap("EndGame");
        rebindOperation = inputReferenceArray[control].inputActions[0].action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(.1f)
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete()
    {
        //int bindingIndex = inputReferenceArray[control].inputActions[0].action.GetBindingIndexForControl(inputReference_.action.controls[0]);
        for (int i = 1; i < inputReferenceArray[control].inputActions.Length; i++)
        {
            inputReferenceArray[control].inputActions[i].action.ApplyBindingOverride(0, inputReferenceArray[control].inputActions[0].action.bindings[0].effectivePath);
        } 
        bindingDisplayNameText[control].text = InputControlPath.ToHumanReadableString(
            inputReferenceArray[control].inputActions[0].action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        
        rebindOperation.Dispose();
        InputController.Instance.SwitchInputMap("PauseGame");
    }

    [Serializable]
    public class inputReferences
    {
        public InputActionReference[] inputActions;
    }
}
