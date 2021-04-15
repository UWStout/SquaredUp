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
        SceneManager.LoadScene("SquaredUp");
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
    }

    private void OnUnpauseGame()
    {
        Time.timeScale = 1.0f;
        InputController.Instance.SwitchInputMap(defaultActionMapName);
        pauseMenu.SetActive(false);
        loreMenu.SetActive(false);
    }
}
