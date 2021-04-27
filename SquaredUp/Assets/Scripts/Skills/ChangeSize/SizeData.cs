using UnityEngine;

/// <summary>Data to describe the size the player will change to when using the ChangeSize skill</summary>
[CreateAssetMenu(fileName = "Size Data", menuName = "ScriptableObjects/SkillData/SizeData")]
public class SizeData : SkillStateData
{
    // Size to change to
    [SerializeField] [Min(0.00001f)] private float size = 1f;
    public float Size { get { return size; } }
}
