using UnityEngine;
using UnityEngine.UI;

public class CheckpointLoader : MonoBehaviour
{
    [SerializeField] private Button loadButton = null;


    private void OnEnable()
    {
        // When the menu is opened, set if the button should be interactable or not
        loadButton.interactable = SaveManager.CheckIfPreviousSaveExists(SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME);
    }


    public void LoadLastCheckpoint()
    {
        SaveManager.LoadGame(SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME);
    }
}
