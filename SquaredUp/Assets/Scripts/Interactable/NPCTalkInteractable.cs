using UnityEngine;
using UnityEngine.Events;

/// <summary>Interactable that lets you talk to an NPC</summary>
public class NPCTalkInteractable : Interactable
{
    // Dialogue for the npc
    [SerializeField] private string[] lines = new string[0];
    [SerializeField] private UnityEvent afterTalkingEvents = null;


    /// <summary>Starts a dialogue with the NPC.</summary>
    public override void InteractAbstract()
    {
        DialogueController.Instance.StartDialogue(lines, afterTalkingEvents.Invoke);
    }
}
