using UnityEngine;

/// <summary>Controls the scale of an object with a size scale and a shape scale.</summary>
public class ScaleController : MonoBehaviour
{
    // References
    // Reference to the scaling component on the object
    [SerializeField] private Transform scalableTrans = null;

    // Scale to apply based on the shape
    private Vector3 shapeScale = Vector3.one;
    public Vector3 ShapeScale
    {
        get { return shapeScale; }
        set
        {
            shapeScale = value;
            ApplyScale();
        }
    }
    // Scale to apply based on the size
    private Vector3 sizeScale = Vector3.one;
    public Vector3 SizeScale
    {
        get { return sizeScale; }
        set
        {
            sizeScale = value;
            ApplyScale();
        }
    }

    // Starting scale of the object
    private Vector3 originalScale = Vector3.one;
    public Vector3 OriginalScale
    {
        get { return originalScale; }
    }


    // Called 1st
    // Initialization
    private void Start()
    {
        originalScale = scalableTrans.localScale;
    }


    /// <summary>Applies the scale changes to the objects's scalable transorm</summary>
    private void ApplyScale()
    {
        scalableTrans.localScale = Vector3.Scale(originalScale, Vector3.Scale(shapeScale, sizeScale));
    }
}
