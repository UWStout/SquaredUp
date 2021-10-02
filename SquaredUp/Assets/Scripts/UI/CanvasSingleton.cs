using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSingleton : MonoBehaviour
{
    // Singleton
    private static CanvasSingleton instance;
    public static CanvasSingleton Instance { get { return instance; } }

    // Completely black covering the canvas
    [SerializeField] private Image blackImage = null;
    // Checkpoint popup
    [SerializeField] private FadeText checkpointPopupText = null;
    // End game menu
    [SerializeField] private GameObject gameOverMenu = null;

    // Functionality to do at different points in the fadeinout coroutine.
    public delegate void FadeOutInFunction();
    // Coroutine info.
    private bool isFadeInOutActive = false;
    private Coroutine fadeInOutCoroutine = null;


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
            Debug.LogError("Cannot have multiple CanvasSingletons");
            Destroy(this.gameObject);
        }
    }


    /// <summary>
    /// Shows the checkpoint reached text and then fades it.
    /// </summary>
    public void ShowCheckpointReached()
    {
        checkpointPopupText.ShowThenFade();
    }

    /// <summary>Shows the game over menu.</summary>
    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    /// <summary>Starts a coroutine to fade the screen to black and then to show everything again.</summary>
    /// <param name="colorChangeSpeed">Speed to fade in and out at.</param>
    /// <param name="afterFadeOut">Delegate to call when we the world turns black.</param>
    /// <param name="afterFadeIn">Delegate to call when the world is shown again.</param>
    public void StartFadeOutAndIn(float colorChangeSpeed, FadeOutInFunction afterFadeOut, FadeOutInFunction afterFadeIn)
    {
        if (isFadeInOutActive)
        {
            StopCoroutine(fadeInOutCoroutine);
        }
        fadeInOutCoroutine = StartCoroutine(FadeOutAndInCoroutine(colorChangeSpeed, afterFadeOut, afterFadeIn));
    }

    /// <summary>Coroutine to fade out (fade to black) and then fade in (show the world again).</summary>
    /// <param name="colorChangeSpeed">Speed to fade in and out at.</param>
    /// <param name="afterFadeOut">Delegate to call when we the world turns black.</param>
    /// <param name="afterFadeIn">Delegate to call when the world is shown again.</param>
    private IEnumerator FadeOutAndInCoroutine(float colorChangeSpeed, FadeOutInFunction afterFadeOut, FadeOutInFunction afterFadeIn)
    {
        isFadeInOutActive = true;

        // Fade out the screen
        Color startColor = Color.black;
        startColor.a = 0;
        while (blackImage.color.a < 1)
        {
            Color CurrentColor = blackImage.color;
            CurrentColor.a += colorChangeSpeed;
            blackImage.color = CurrentColor;
            yield return null;
        }
        startColor.a = 1;
        blackImage.color = startColor;

        // While the screen is completely black do the given do while dark behavior
        afterFadeOut?.Invoke();

        // Fade in the screen
        while (blackImage.color.a > 0)
        {
            Color CurrentColor = blackImage.color;
            CurrentColor.a -= colorChangeSpeed;
            blackImage.color = CurrentColor;
            yield return null;
        }
        startColor.a = 0;
        blackImage.color = startColor;

        // Call the after fade in behavior
        afterFadeIn?.Invoke();

        isFadeInOutActive = false;
        yield return null;
    }
}
