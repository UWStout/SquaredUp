using UnityEngine;

public class PositionResetter : MonoBehaviour
{
    private Vector3 _originalPosition = Vector3.zero;


    // Start is called before the first frame update
    private void Start()
    {
        _originalPosition = transform.position;
    }


    public void ResetPosition()
    {
        transform.position = _originalPosition;
    }
}
