using UnityEngine;
using UnityEngine.InputSystem;

// Manages the dialogue box and typewriter
public class DialogueController : MonoBehaviour
{
    // Input References
    // Reference to the player input
    [SerializeField]
    private PlayerInput playerInputRef = null;
    // Name of the dialogue action map in player input
    [SerializeField]
    private string dialogueActionMapName = "Dialogue";
    // Name of the player action map in player input
    [SerializeField]
    private string playerActionMapName = "Player";

    // UI References
    // Object that shows the textbox when active
    [SerializeField]
    private GameObject uiComponent = null;
    // Type writer to help display text
    [SerializeField]
    private TypeWriter typeWriteRef = null;

    // The lines that will be written in each text box
    private string[] dialogueLines;
    // Current line index
    private int curLineIndex;

    // If the type writer has finished
    private bool finishedTyping;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.AdvanceDialogueEvent += OnAdvanceDialogue;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.AdvanceDialogueEvent -= OnAdvanceDialogue;
    }


    /// <summary>
    /// Starts the dialogue
    /// </summary>
    /// <param name="lines">Lines to show in the text box</param>
    public void StartDialogue(string[] lines)
    {
        // Swap input map
        playerInputRef.SwitchCurrentActionMap(dialogueActionMapName);
        // Show text box
        uiComponent.SetActive(true);
        // Initialization
        dialogueLines = lines;
        curLineIndex = 0;
        // Start typing
        StartTyping();
    }

    /// <summary>
    /// Called when player inputs next or AdvanceDialogue
    /// </summary>
    public void OnAdvanceDialogue()
    {
        // Type next line if finished typing
        if (finishedTyping)
        {
            // If there are more lines, continue the dialogue
            if (curLineIndex < dialogueLines.Length)
            {
                // Type next line.
                StartTyping();
            }
            else
            {
                // End the dialogue.
                EndDialogue();
            }
        }
        // End the typing early
        else
        {
            typeWriteRef.PreemptiveLineFinish();
        }
    }

    /// <summary>
    /// Calls the type writer to start typing and sets finishedTyping to false.
    /// </summary>
    private void StartTyping()
    {
        finishedTyping = false;
        typeWriteRef.TypeLine(dialogueLines[curLineIndex], HandleFinishTyping);
    }

    /// <summary>
    /// Sets finishedTyping to true and increments the line index.
    /// </summary>
    private void HandleFinishTyping()
    {
        ++curLineIndex;
        finishedTyping = true;
    }

    /// <summary>
    /// Ends the dialogue
    /// </summary>
    private void EndDialogue()
    {
        // Reset some variables
        dialogueLines = null;
        // Hide text box
        uiComponent.SetActive(false);
        // Swap input map back
        playerInputRef.SwitchCurrentActionMap(playerActionMapName);
    }

    
}
