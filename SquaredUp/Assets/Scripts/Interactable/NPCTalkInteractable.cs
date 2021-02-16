using UnityEngine;

public class NPCTalkInteractable : Interactable
{
    // Reference to the dialogue controller
    [SerializeField]
    private DialogueController dialogueContRef = null;
    // Dialogue for the npc
    [SerializeField]
    private string[] lines = new string[0];

    // Reference to the alert for the npc
    [SerializeField]
    private GameObject npcAlertObj = null;

    /// <summary>
    /// Starts a dialogue with the NPC.
    /// </summary>
    public override void Interact() {
        base.Interact();
        dialogueContRef.StartDialogue(lines);
    }

    /// <summary>
    /// Show NPC tag
    /// </summary>
    public override void DisplayAlert() {
        npcAlertObj.SetActive(true);
    }

    /// <summary>
    /// Hide NPC tag
    /// </summary>
    public override void HideAlert() {
        npcAlertObj.SetActive(false);
    }
}
