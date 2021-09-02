using UnityEngine;

/// <summary>Maintains the outline width of things with outlines in the editor.</summary>
[ExecuteInEditMode]
public class MaintainOutlineWidthEditor : MonoBehaviour
{
#if UNITY_EDITOR
    // Non outline part of the thing.
    [SerializeField] private Transform body = null;
    // Outline of the thing.
    [SerializeField] private Transform outline = null;
    // Thickness of the outline.
    [SerializeField] [Min(0)] private float outlineWidth = 0.2f;
    [SerializeField] private bool ignoreZ = true;


    // Called when something is changed in the editor.
    private void Update()
    {
        SetGlobalScale(body, outline.lossyScale - new Vector3(outlineWidth, outlineWidth, outlineWidth));
    }

    /// <summary>Sets the global scale of the given transform to the given scale.</summary>
    /// <param name="trans">Transform to set scale of.</param>
    /// <param name="globalScale">Global scale to set transform to.</param>
    public void SetGlobalScale(Transform trans, Vector3 globalScale)
    {
        Vector3 oneV = new Vector3(1, 1, trans.localScale.z);
        if (!ignoreZ)
        {
            oneV.z = 1;
        }
        trans.localScale = oneV;
        Vector3 scale = new Vector3(globalScale.x / trans.lossyScale.x,
            globalScale.y / trans.lossyScale.y, trans.localScale.z);
        if (!ignoreZ)
        {
            scale.z = globalScale.z / trans.lossyScale.z;
        }
        trans.localScale = scale;
    }
#endif //UNITY_EDITOR
}
