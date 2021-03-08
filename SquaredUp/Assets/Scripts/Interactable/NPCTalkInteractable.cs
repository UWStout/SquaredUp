using UnityEngine;
 
/// <summary>Interactable that lets you talk to an NPC</summary>
public class NPCTalkInteractable : Interactable
{
    // Dialogue for the npc
    [SerializeField] private string[] lines = new string[0];
    // Reference to the alert for the npc
    [SerializeField] private GameObject npcAlertObj = null;

    /// <summary>Starts a dialogue with the NPC.</summary>
    public override void Interact()
    {
        DialogueController.Instance.StartDialogue(lines);
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
