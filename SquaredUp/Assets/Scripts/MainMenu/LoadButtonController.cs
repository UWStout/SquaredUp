using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Will not allow the player to interact with the load button if there is no save data.
/// </summary>
public class LoadButtonController : MonoBehaviour
{
    // Reference to the load button that will be either enabled
    // or disabled depending on if there is save data or not
    [SerializeField] private Button loadGameButton = null;

   
    // Called 1st
    // Initialization
    private void Start()
    {
        loadGameButton.interactable = SaveManager.CheckIfPreviousSaveExists();
    }
}
