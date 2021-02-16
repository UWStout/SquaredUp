using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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
    // Text to display the dialogue with
    [SerializeField]
    private TextMeshProUGUI typeText = null;

    // Delay between typing characters
    [SerializeField]
    private float delayBetweenLetters = 0.15f;

    // The lines that will be written in each text box
    private string[] dialogueLines;
    // Current line index
    private int curLineIndex;

    // Reference to running coroutine
    private Coroutine typeWriteCoroutine;
    // If the type writer has finished
    private bool finishedTyping;

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
        TypeLine();
    }

    /// <summary>
    /// Ends the dialogue
    /// </summary>
    private void EndDialogue()
    {
        // Reset some variables
        typeText.text = "";
        dialogueLines = null;
        // Hide text box
        uiComponent.SetActive(false);
        // Swap input map back
        playerInputRef.SwitchCurrentActionMap(playerActionMapName);
    }

    /// <summary>
    /// Called when player inputs next or AdvanceDialogue
    /// </summary>
    public void ProcessAdvanceCall(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            // Type next line if finished typing
            if (finishedTyping)
            {
                // Try to type the next line, if we cannot, end the dialogue
                if (!TypeLine())
                {
                    EndDialogue();
                }
            }
            // End the typing early
            else
            {
                PreemptiveLineFinish();
            }
        }
    }

    /// <summary>
    /// Type the current line
    /// </summary>
    /// <returns>False if out of lines to type</returns>
    private bool TypeLine()
    {
        if (curLineIndex < dialogueLines.Length)
        {
            typeText.text = "";
            typeWriteCoroutine = StartCoroutine(TypeWriter());
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Coroutine to make the letters appear slowly
    /// </summary>
    private IEnumerator TypeWriter()
    {
        finishedTyping = false;
        string line = dialogueLines[curLineIndex];
        // Indiivually write each character
        foreach (char c in line)
        {
            TypeOneLetter(c);
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        // Increment line and set finished tpying to true
        finishedTyping = true;
        ++curLineIndex;
        yield return null;
    }

    /// <summary>
    /// Appends the given letter to the text
    /// </summary>
    /// <param name="c">Character to append</param>
    private void TypeOneLetter(char c)
    {
        typeText.text += c;
    }

    /// <summary>
    /// Show the whole line before the typewriter is done
    /// </summary>
    private void PreemptiveLineFinish()
    {
        StopCoroutine(typeWriteCoroutine);
        typeText.text = dialogueLines[curLineIndex];
        finishedTyping = true;
        ++curLineIndex;
    }
}
