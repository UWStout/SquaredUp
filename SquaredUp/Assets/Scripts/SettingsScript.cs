using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private inputReferences[] inputReferenceArray;
    [SerializeField] private TMP_Text[] bindingDisplayNameText;
    private InputActionReference inputReference_;
    private int control;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation, rebindCompositeOperation, rebindLoopingOperation;
    private string[] move = { "Up", "Down", "Left", "Right" };

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;


    private void Start()
    {
        resolutions=Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetVolume(float volume_)
    {
        audioMixer.SetFloat("Volume", volume_);
    }

    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
    }

    public void StartRebindingComposite(int control_) 
    {
        control = control_;
        InputController.Instance.SwitchInputMap("EndGame");



        rebindCompositeOperation = inputReferenceArray[0].inputActions[0].action.PerformInteractiveRebinding()
            .WithTargetBinding(control)
            .WithExpectedControlType("Button")
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(.1f)
            .OnComplete(operation => RebindCompositeComplete())
            .Start();

    }


    private void RebindCompositeComplete()
    {
        Debug.Log(inputReferenceArray[0].inputActions[1].action);
        inputReferenceArray[0].inputActions[1].action.ApplyBindingOverride(control, inputReferenceArray[0].inputActions[0].action.bindings[control].effectivePath);

        bindingDisplayNameText[control-1].text = InputControlPath.ToHumanReadableString(
            inputReferenceArray[control-1].inputActions[0].action.bindings[control].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        rebindCompositeOperation.Dispose();
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
