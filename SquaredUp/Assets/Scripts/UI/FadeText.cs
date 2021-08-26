using System.Collections;
using UnityEngine;

using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FadeText : MonoBehaviour
{
    [SerializeField] [Range(0.00000001f, 1.0f)] private float fadeSpeed = 0.005f;

    private TextMeshProUGUI textToFade = null;

    // Coroutine info
    private bool isFadeCoroutineActive = false;
    private Coroutine fadeCoroutine = null;


    // Called 0th
    // Domestic Initialization
    private void Awake()
    {
        textToFade = GetComponent<TextMeshProUGUI>();
    }


    public void ShowThenFade()
    {
        textToFade.enabled = true;
        StartFadeCoroutine();
    }


    private void StartFadeCoroutine()
    {
        if (isFadeCoroutineActive)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine());
    }
    private IEnumerator FadeCoroutine()
    {
        isFadeCoroutineActive = true;

        Color startColor = textToFade.color;

        float t = 0;
        while (t < 1)
        {
            textToFade.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t);
            t += fadeSpeed;
            yield return null;
        }
        textToFade.enabled = false;

        isFadeCoroutineActive = false;
    }
}
