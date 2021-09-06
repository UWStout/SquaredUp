using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameInteractable : Interactable
{
    [SerializeField] private string endGameInputMapName = "EndGame";

    public override void InteractAbstract()
    {
        InputController.Instance.SwitchInputMap(endGameInputMapName);
        CanvasSingleton.Instance.StartFadeOutAndIn(0.01f, CenterPlayer, null);
    }

    private void CenterPlayer()
    {
        PlayerMovement.Instance.GetComponent<GridMover>().enabled = false;
        EndGameController.Instance.PlayEndGameAnimation();
    }
}
