using UnityEngine;

/// <summary>
/// Initializes the CircleChange scriptable object
/// </summary>
public class CircleChangeInitializer : MonoBehaviour
{
    // Reference to the player's circle wall collision object
    [SerializeField] private GameObject playerCircleWallColliderObj = null;
    // Reference to the circle change behavior that we will give the circle wall collision object
    [SerializeField] private CircleChange circleChangeBehave = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        circleChangeBehave.PlayerCircleWallColliderObj = playerCircleWallColliderObj;
    }
}
