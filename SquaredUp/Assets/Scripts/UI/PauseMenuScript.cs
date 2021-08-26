using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject loreMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject controlMenu;
    [SerializeField]
    private string pauseActionMapName = "PauseGame";
    [SerializeField]
    private string defaultActionMapName = "Player";

    [SerializeField] private FadeText saveCompleteText = null;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            showCursor();
        } else
        {
            hideCursor();
        }
    }

    private void OnEnable()
    {
        InputEvents.PauseGameEvent += OnPauseGame;
        InputEvents.UnpauseGameEvent += OnUnpauseGame;
    }

    private void OnDisable()
    {
        InputEvents.PauseGameEvent -= OnPauseGame;
        InputEvents.UnpauseGameEvent -= OnUnpauseGame;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SquaredUp_Combined");
    }

    public void OpenPauseFromLore()
    {
        FlipMenu(loreMenu, pauseMenu);
    }

    public void OpenLoreFromPause()
    {
        loreMenu.GetComponent<LoreMenu>().UpdateLore();
        FlipMenu(pauseMenu, loreMenu);
    }

    public void ResumeGame()
    {
        OnUnpauseGame();
    }

    public void OpenControlFromPause()
    {
        FlipMenu(controlMenu, pauseMenu);
    }

    public void OpenPauseFromControl()
    {
        FlipMenu(pauseMenu, controlMenu);
    }

    public void GoToScene(string sceneName)
    {
        OnUnpauseGame();
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Saves the current scene's game data.
    /// </summary>
    public void SaveGame()
    {
        SaveManager.SaveGame();
        saveCompleteText.ShowThenFade();
    }

    private void FlipMenu(GameObject g, GameObject o)
    {
        g.SetActive(!g.activeSelf);
        o.SetActive(!o.activeSelf);
    }

    private void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        InputController.Instance.SwitchInputMap(pauseActionMapName);
        pauseMenu.SetActive(true);
        loreMenu.SetActive(false);
        controlMenu.SetActive(false);
        showCursor();
    }

    private void OnUnpauseGame()
    {
        Time.timeScale = 1.0f;
        InputController.Instance.PopInputMap(pauseActionMapName);
        pauseMenu.SetActive(false);
        loreMenu.SetActive(false);
        controlMenu.SetActive(false);
        hideCursor();
    }

    private void showCursor()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    private void hideCursor()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }
}
