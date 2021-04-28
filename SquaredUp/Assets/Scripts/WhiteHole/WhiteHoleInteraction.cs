using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleInteraction : Interactable
{
    // Point of rotation
    [SerializeField] GameObject rotatingCenter = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject whiteHole = null;


    /// <summary>Starts a dialogue with the NPC.</summary>
    public override void Interact()
    {
        whiteHole.transform.GetChild(1).gameObject.SetActive(false);
        player.transform.position = Vector3.MoveTowards(player.transform.position, whiteHole.transform.position, 1);
    }

}
