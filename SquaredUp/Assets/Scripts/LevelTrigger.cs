using UnityEngine;

/// <summary>
/// To be placed on the level triggers to set levels active and inactive.
/// Should be on a layer that only collides with the player.
/// </summary>
public class LevelTrigger : MonoBehaviour
{
    // Level to load
    [SerializeField] private GameObject levelToLoad = null;
    // Level to unload
    [SerializeField] private GameObject levelToHide = null;

    // Load the level and hide the other one.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelToLoad.SetActive(true);
        levelToHide.SetActive(false);
    }
}
