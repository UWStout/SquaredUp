using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles swapping of the input map to handle when some scripts swap and then want to swap back to what it previously was.
/// </summary>
public class InputController : MonoBehaviour
{
    // Singleton
    private static InputController instance;
    public static InputController Instance { get { return instance; } }

    // Reference to the player input
    [SerializeField] private PlayerInput playerInputRef = null;
    // Name of the default map
    [SerializeField] private string defaultMapName = "Player";

    // Stack of active input maps.
    private List<string> activeMapNames = new List<string>();


    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple InputControllers");
            Destroy(this);
        }
    }

    // Called 1st
    // Initialization
    private void Start()
    {
        // Initialize active map names to have the default map in it.
        activeMapNames = new List<string>();
        activeMapNames.Add(defaultMapName);
        UpdateActiveInputMap();
    }


    /// <summary>Adds the given input map to the stack and swaps to it.</summary>
    /// <param name="inputMapName">Name of the input map to add to the stack.</param>
    public void SwitchInputMap(string inputMapName)
    {
        activeMapNames.Add(inputMapName);
        UpdateActiveInputMap();
    }

    /// <summary>Pops the input map with the given name off the stack and updates the input map to the one before it.</summary>
    /// <param name="inputMapName">Name of the input map to remove from the stack.</param>
    public void PopInputMap(string inputMapName)
    {
        activeMapNames.Remove(inputMapName);
        UpdateActiveInputMap();
    }

    /// <summary>Updates the action map to be the map at the top of the stack.</summary>
    private void UpdateActiveInputMap()
    {
        if (activeMapNames.Count > 0)
        {
            playerInputRef.SwitchCurrentActionMap(activeMapNames[activeMapNames.Count - 1]);
        }
        else
        {
            Debug.LogError("Last input map was removed from the stack");
        }
    }
}
