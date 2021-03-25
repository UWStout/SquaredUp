using UnityEngine;

/// <summary>Places the after ui element directly to the right of the before ui element on start.</summary>
public class PositionTextAfterOtherText : MonoBehaviour
{
    // Element to place the after ui element after
    [SerializeField] private RectTransform beforeUIElement = null;
    // Element to position after the before ui element
    [SerializeField] private RectTransform afterUIElement = null;
    // Offset for where the element should be place after the before element
    [SerializeField] private Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the center right of the before text
        Vector2 beforeRectCenter = beforeUIElement.rect.center;
        Vector3 beforeCenter = beforeUIElement.position + new Vector3(beforeRectCenter.x, beforeRectCenter.y);
        float beforeRightX = beforeCenter.x + beforeUIElement.offsetMax.x;
        Vector3 beforeCenterRight = new Vector3(beforeRightX, beforeCenter.y);

        // Get the position we should place the after text at
        float afterHalfSizeX = -beforeUIElement.offsetMin.x;
        Vector3 afterPlacePos = beforeCenterRight + new Vector3(afterHalfSizeX, 0) + offset;

        // Place the text there
        afterUIElement.position = afterPlacePos;
    }
}
