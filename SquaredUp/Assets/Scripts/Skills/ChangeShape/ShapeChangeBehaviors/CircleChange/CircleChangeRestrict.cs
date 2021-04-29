using UnityEngine;

/// <summary>
/// Restricts shape changing when inside a circle only area.
/// </summary>
public class CircleChangeRestrict : MonoBehaviour
{
    // Reference to the circle change behavior that we will give the circle wall collision object
    [SerializeField] private CircleChange circleChangeBehave = null;
    
    // Amount of colliders this is inside
    private int colAmount = 0;


    // Called 1st
    // Initialization
    private void Start()
    {
        circleChangeBehave.Reset();
        colAmount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter");
        // When inside a circle only zone, cannot change to not a circle.
        circleChangeBehave.RestrictChange = true;
        ++colAmount;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop restricting when leaving last non circle zone
        --colAmount;
        if (colAmount <= 0)
        {
            colAmount = 0;
            circleChangeBehave.RestrictChange = false;
        }
    }
}
