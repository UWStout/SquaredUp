using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameInteractable : Interactable
{
    [SerializeField] private string endGameInputMapName = "EndGame";

    public override void Interact()
    {
        InputController.Instance.SwitchInputMap(endGameInputMapName);
        CanvasSingleton.Instance.StartFadeOutAndIn(0.01f, CenterPlayer, null);
    }

    private void CenterPlayer()
    {
        EndGameController.Instance.PlayEndGameAnimation();
    }
}
