using UnityEngine;
 
/// <summary>Interactable that lets you talk to an NPC</summary>
public class NPCTalkInteractable : Interactable
{
    // Dialogue for the npc
    [SerializeField] private string[] lines = new string[0];
    // Reference to the alert for the npc
    [SerializeField] private GameObject npcAlertObj = null;

    // Reference to the dialogue controller
    private DialogueController dialogueContRef = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        dialogueContRef = FindObjectOfType<DialogueController>();
        if (dialogueContRef == null)
        {
            Debug.LogError("NPCTalkInteractable could not find DialogueController");
        }
    }

    /// <summary>Starts a dialogue with the NPC.</summary>
    public override void Interact()
    {
        dialogueContRef.StartDialogue(lines);
    }

    /// <summary>Show NPC tag</summary>
    public override void DisplayAlert()
    {
        npcAlertObj.SetActive(true);
    }

    /// <summary>Hide NPC tag</summary>
    public override void HideAlert()
    {
        npcAlertObj.SetActive(false);
    }
}
