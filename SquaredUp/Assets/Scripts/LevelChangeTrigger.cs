using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When the player collides with this object, the scene is changed.
/// Should be on a layer that only collides with the player.
/// </summary>
public class LevelChangeTrigger : MonoBehaviour
{
    // Name of the scene to change to
    [SerializeField] private string sceneName = "Scene";

    // Called when this collides with something else
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
