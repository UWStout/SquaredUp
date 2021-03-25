using UnityEngine;

public class PositionTextAfterOtherText : MonoBehaviour
{
    [SerializeField] private RectTransform beforeUIElement = null;
    [SerializeField] private RectTransform afterUIElement = null;
    [SerializeField] private Vector3 offset = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the center right of the before text
        Vector2 beforeRectPos = beforeUIElement.rect.position;
        Vector3 beforeCenter = beforeUIElement.position + new Vector3(beforeRectPos.x, 0);
        float beforeRightX = beforeCenter.x + beforeUIElement.rect.width / 2;
        Vector3 beforeCenterRight = new Vector3(beforeRightX, beforeCenter.y);
        Debug.Log("BeforeCenterRight: " + Vector3DetailedToString(beforeCenterRight));

        // Get the position we should place the after text at
        float afterHalfSizeX = beforeUIElement.rect.width / 2;
        Vector3 afterPlacePos = beforeCenterRight + new Vector3(afterHalfSizeX, 0) + offset;
        Debug.Log("AfterPlacePos: " + Vector3DetailedToString(afterPlacePos));

        // Place the text there
        afterUIElement.position = afterPlacePos;
    }

    private string Vector3DetailedToString(Vector3 vector)
    {
        return "(" + vector.x + ", " + vector.y + ", " + vector.z + ")";
    }
}
