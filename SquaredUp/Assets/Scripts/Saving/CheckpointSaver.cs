using UnityEngine;

/// <summary>
/// When the player enteres a trigger with this script attached,
/// it will save the game for loading the checkpoint later.
/// Does not save if the current checkpoint has a lower checkpoint index
/// than the last saved checkpoint.
/// </summary>
public class CheckpointSaver : MonoBehaviour
{
    private static int currentlySavedCheckpoint = int.MinValue;

    [SerializeField] private int checkpointIndex = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collision if its not the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Don't save if the checkpoint is lower priority
        if (checkpointIndex <= currentlySavedCheckpoint)
        {
            return;
        }

        // Don't show checkpoint reached if this is the first checkpoint (dummy checkpoint)
        // or if this checkpoint is the first one after loading into the game
        if (currentlySavedCheckpoint != int.MinValue)
        {
            CanvasSingleton.Instance.ShowCheckpointReached();
        }

        // Save the checkpoint and update the checpoint index
        SaveManager.SaveGame(SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME);
        currentlySavedCheckpoint = checkpointIndex;
    }
}
