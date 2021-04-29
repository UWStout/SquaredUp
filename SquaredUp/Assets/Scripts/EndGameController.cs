using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    // Singleton
    private static EndGameController instance = null;
    public static EndGameController Instance
    {
        get { return instance; }
    }

    // Script that makes the camera follow the player.
    [SerializeField] private Follow cameraFollow = null;
    // Reference to the animator on the master parent of the player objects.
    [SerializeField] private Animator playerCollectionAnimator = null;
    // Position to move the player to at the end of the game.
    [SerializeField] private Vector3 endGamePosition = Vector3.zero;
    // Music to play at the end.
    [SerializeField] private AudioSource endMusicSource = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple EndGameContollers in the scene");
            Destroy(this.gameObject);
        }
    }


    public void PlayEndGameAnimation()
    {
        PlayerMovement.Instance.transform.position = endGamePosition;
        MusicManager.Instance.gameObject.SetActive(false);
        endMusicSource.Play();
        StopTopDownFollow();
        playerCollectionAnimator.enabled = true;
        playerCollectionAnimator.SetBool("endGame", true);
    }


    /// <summary>Stops following the player via script.</summary>
    private void StopTopDownFollow()
    {
        cameraFollow.enabled = false;
    }

    private void FinishEndGameAnimation()
    {
        playerCollectionAnimator.SetBool("endGameAfter", true);
        CanvasSingleton.Instance.ShowGameOverMenu();
    }
}
