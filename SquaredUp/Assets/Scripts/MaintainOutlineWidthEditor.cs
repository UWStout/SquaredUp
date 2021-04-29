using UnityEngine;

/// <summary>Maintains the outline width of things with outlines in the editor.</summary>
[ExecuteInEditMode]
public class MaintainOutlineWidthEditor : MonoBehaviour
{
    // Non outline part of the thing.
    [SerializeField] private Transform body = null;
    // Outline of the thing.
    [SerializeField] private Transform outline = null;
    // Thickness of the outline.
    [SerializeField] [Min(0)] private float outlineWidth = 0.2f;


    // Called when something is changed in the editor.
    private void Update()
    {
        SetGlobalScale(body, outline.lossyScale - new Vector3(outlineWidth, outlineWidth, outlineWidth));
    }

    /// <summary>Sets the global scale of the given transform to the given scale.</summary>
    /// <param name="transform">Transform to set scale of.</param>
    /// <param name="globalScale">Global scale to set transform to.</param>
    public void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
