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
    [SerializeField] private Slider VolumeSlider;
    [SerializeField] private GameObject BlackScreen;
    [SerializeField] private GameObject ControlMenu;
    private InputActionReference inputReference_;
    private int control;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation, rebindCompositeOperation, rebindLoopingOperation;
    private string[] move = { "Up", "Down", "Left", "Right" };

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;


    private void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            //Debug.Log(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        foreach (inputReferences _a in inputReferenceArray)
        { 
            control = (System.Array.IndexOf(inputReferenceArray, _a));
            
            if (!string.IsNullOrWhiteSpace(PlayerPrefs.GetString(control.ToString())))
            {
                if (control <= 3)
                {
                    string _temp = PlayerPrefs.GetString(control.ToString());
                    control++;
                    _a.inputActions[0].action.ApplyBindingOverride(control, _temp);
                    RebindCompositeComplete();
                }
                else
                {
                    string _temp = PlayerPrefs.GetString(control.ToString());
                    _a.inputActions[0].action.ApplyBindingOverride(0, _temp);
                    RebindComplete();
                }
            }
        }

        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        VolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Volume"));
        SetResolution(options.IndexOf(PlayerPrefs.GetString("Resolution")));
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < resolutions.Length && resolutionIndex>0)
        {
            bool revert = false;
            if (ControlMenu.activeSelf)
            {
                BlackScreen.SetActive(true);
                ControlMenu.SetActive(false);
                revert = true;
            }
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            PlayerPrefs.SetString("Resolution", resolution.ToString());
            if (revert)
            {
                ControlMenu.SetActive(true);
                BlackScreen.SetActive(false);
            }
        }
    }

    public void SetVolume(float volume_)
    {
        audioMixer.SetFloat("Volume", volume_);
        PlayerPrefs.SetFloat("Volume", volume_);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFull)
    {
        bool revert = false;
        if (ControlMenu.activeSelf)
        {
            BlackScreen.SetActive(true);
            ControlMenu.SetActive(false);
            revert = true;
        }
        Screen.fullScreen = isFull;
        if (revert)
        {
            ControlMenu.SetActive(true);
            BlackScreen.SetActive(false);
        }
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
        bindingDisplayNameText[control-1].text = InputControlPath.ToHumanReadableString(
            inputReferenceArray[control-1].inputActions[0].action.bindings[control].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        PlayerPrefs.SetString((control-1).ToString(), inputReferenceArray[0].inputActions[0].action.bindings[control].effectivePath);
        PlayerPrefs.Save();
        if (rebindCompositeOperation != null)
        {
            rebindCompositeOperation.Dispose();
        }
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

        PlayerPrefs.SetString(control.ToString(), inputReferenceArray[control].inputActions[0].action.bindings[0].effectivePath);
        PlayerPrefs.Save();
        if (rebindOperation!=null)
        {
            rebindOperation.Dispose();
        }
        InputController.Instance.SwitchInputMap("PauseGame");
    }

    [Serializable]
    public class inputReferences
    {
        public InputActionReference[] inputActions;
    }
}
