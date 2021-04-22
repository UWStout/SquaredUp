using UnityEngine;

/// <summary>Base class for any custom scripts for the UI Prefabs that want to override this behavior.</summary>
public abstract class StateUIBehavior : MonoBehaviour
{
    // Reference tot he GridHUDManager
    [SerializeField] private HUDManager hudManager = null;
    protected HUDManager HUDManager { get { return hudManager; } }

    /// <summary>Sets the hud manager reference for the state UI's behavior.</summary>
    /// <param name="hudMan">Reference tot he GridHUDManager.</param>
    public void Initialize(HUDManager hudMan)
    {
        hudManager = hudMan;
    }
}
