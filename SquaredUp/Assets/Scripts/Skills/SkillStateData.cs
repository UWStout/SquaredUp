using UnityEngine;

/// <summary>Base class for data classes that define a skill's state</summary>
public abstract class SkillStateData : ScriptableObject
{
    // UI Prefab to display for the state
    [SerializeField] private GameObject uiElement = null;
    public GameObject UIElement { get { return uiElement; } }
}
