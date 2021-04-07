using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject loreMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private string pauseActionMapName = "PauseGame";
    [SerializeField]
    private string defaultActionMapName = "Player";
    [SerializeField] 
    private PlayerInput playerInputRef = null;


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

    public void OpenMainFromPause()
    {
        Application.LoadLevel("MainMenu");
    }

    public void StartGame()
    {
        Application.LoadLevel("SquaredUp");
    }

    public void OpenPauseFromLore()
    {
        FlipMenu(loreMenu, pauseMenu);
    }

    public void OpenLoreFromPause()
    {
        FlipMenu(pauseMenu, loreMenu);
    }

    public void ResumeGame()
    {
        OnUnpauseGame();
    }

    private void FlipMenu(GameObject g, GameObject o)
    {
        g.SetActive(!g.activeSelf);
        o.SetActive(!o.activeSelf);
    }

    private void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        playerInputRef.SwitchCurrentActionMap(pauseActionMapName);
        pauseMenu.SetActive(true);
    }

    private void OnUnpauseGame()
    {
        Time.timeScale = 1.0f;
        playerInputRef.SwitchCurrentActionMap(defaultActionMapName);
        pauseMenu.SetActive(false);
        loreMenu.SetActive(false);
    }
}
