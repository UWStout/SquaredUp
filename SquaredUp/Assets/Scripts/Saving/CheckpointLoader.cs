using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using NaughtyAttributes;

public class CheckpointLoader : MonoBehaviour
{
    [SerializeField] private PauseMenuScript m_pauseMenu = null;
    [SerializeField] private Button loadButton = null;
    [SerializeField] [Scene] private string m_checkpointLoadSceneName = "LoadCheckpoint";


    private void OnEnable()
    {
        // When the menu is opened, set if the button should be interactable or not
        loadButton.interactable = SaveManager.CheckIfPreviousSaveExists(SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME);
    }


    public void LoadLastCheckpoint()
    {
        // We don't do this anymore, we just load a different scene which will cause this to happen
        //SaveManager.LoadGame(SaveManager.CHECKPOINT_SAVE_DATA_FILE_NAME);

        // Unpause the game
        m_pauseMenu.ResumeGame();
        // Load the scene
        SceneManager.LoadScene(m_checkpointLoadSceneName);
    }
}
