using UnityEngine;
 
/// <summary>Interactable that lets you talk to an NPC</summary>
public class NPCTalkInteractable : Interactable
{
    // Dialogue for the npc
    [SerializeField] private string[] lines = new string[0];
    [SerializeField] GameObject vision = null;


    /// <summary>Starts a dialogue with the NPC.</summary>
    public override void Interact()
    {
        DialogueController.Instance.StartDialogue(lines);
        if (vision!= null)
        {
            vision.SetActive(false);
        }
    }
}
