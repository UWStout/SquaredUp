using UnityEngine;

/// <summary>
/// Calls dont destroy on load for the gameobject
/// </summary>
public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
