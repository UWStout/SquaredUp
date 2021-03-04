using UnityEngine;

///<summary>Data to describe the amount to zoom for the zoom skill</summary>
[CreateAssetMenu(fileName = "Zoom Data", menuName = "ScriptablesObjects/SkillData/ZoomData")]
public class ZoomData : SkillStateData
{
    // Amount to zoom
    [SerializeField] private float zoomAmount = 0f;
    public float ZoomAmount { get { return zoomAmount; } }
}
