using UnityEngine;

/// <summary>Controls the scale of an object with a size scale and a shape scale.</summary>
public class ScaleController : MonoBehaviour
{
    // References
    // Reference to the scaling component on the object
    [SerializeField] private Transform scalableTrans = null;

    // Starting localScale of the scalable transform
    // Can't simply get it on awake or start because of loading from save creates race condition
    [SerializeField] private Vector2Int originalScale = new Vector2Int(1, 1);
    public Vector2Int OriginalScale
    {
        get { return originalScale; }
    }

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


    public Vector3 GetFullScale()
    {
        Vector3 originalScaleV3 = new Vector3(OriginalScale.x, OriginalScale.y, 1);
        return Vector3.Scale(originalScaleV3, Vector3.Scale(ShapeScale, SizeScale));
    }


    /// <summary>Applies the scale changes to the objects's scalable transorm</summary>
    private void ApplyScale()
    {
        Vector3 originScaleV3 = new Vector3(originalScale.x, originalScale.y, 1);
        scalableTrans.localScale = Vector3.Scale(originScaleV3, Vector3.Scale(shapeScale, sizeScale));
    }
}
