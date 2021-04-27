using UnityEngine;

/// <summary>Data to describe the color/material the player will change to in the change color skill</summary>
[CreateAssetMenu(fileName = "Color Data", menuName = "ScriptableObjects/SkillData/ColorData")]
public class ColorData : SkillStateData
{
    // Material of the color to change to
    [SerializeField] private Material mat;
    public Material Material { get { return mat; } }
}
