using System.Collections;
using UnityEngine;
using TMPro;

// Controls the text on this object to append characters one at a time.
[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeWriter : MonoBehaviour
{
    // Delay between typing characters
    [SerializeField]
    private float delayBetweenLetters = 0.15f;

    // Text to write to
    private TextMeshProUGUI typeText;

    // Curent line being written
    private string currentLine;
    // Event to call when finished typing
    public delegate void FinishWriting();
    private event FinishWriting OnFinishWriting;

    // Reference to running coroutine
    private Coroutine typeWriteCoroutine;

    // Called 0th. Set references.
    private void Awake()
    {
        typeText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Type the current line
    /// </summary>
    public void TypeLine(string line, FinishWriting onFinish)
    {
        currentLine = line;
        typeText.text = "";
        OnFinishWriting = onFinish;
        typeWriteCoroutine = StartCoroutine(TypeLineCoroutine());
    }

    /// <summary>
    /// Show the whole line before the typewriter is done
    /// </summary>
    public void PreemptiveLineFinish()
    {
        StopCoroutine(typeWriteCoroutine);
        typeText.text = currentLine;
        FinishTyping();
    }

    /// <summary>
    /// Coroutine to make the letters appear slowly
    /// </summary>
    private IEnumerator TypeLineCoroutine()
    {
        // Indiivually write each character
        foreach (char c in currentLine)
        {
            TypeOneLetter(c);
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        // Finish Typing.
        FinishTyping();
        yield return null;
    }

    /// <summary>
    /// Appends the given letter to the text
    /// </summary>
    /// <param name="c">Character to append</param>
    private void TypeOneLetter(char c)
    {
        typeText.text += c;
    }

    /// <summary>
    /// Sets finishedTyping to true and calls the OnFinishWriting event.
    /// </summary>
    private void FinishTyping()
    {
        OnFinishWriting?.Invoke();
        OnFinishWriting = null;
    }
}
