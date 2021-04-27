using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>Controls the text on this object to append characters one at a time</summary>
[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(AudioSource))]
public class TypeWriter : MonoBehaviour
{
    // Sound effect during dialogue
    public AudioSource dialogue_sfx;
    
    // Delay between typing characters
    [SerializeField] private float delayBetweenLetters = 0.15f;
    // Size not to grow larger than
    [SerializeField] private float maxSentenceSize = 1f;

    // Text to write to
    private TextMeshProUGUI typeText = null;

    // Curent line being written
    private string currentLine = "";
    // Event to call when finished typing
    public delegate void FinishWriting();
    private FinishWriting OnFinishWriting;

    // Reference to running coroutine
    private Coroutine typeWriteCoroutine = null;
    private bool isTypeCoroutineActive = false;

    // Called 0th. Set references.
    private void Awake()
    {
        typeText = GetComponent<TextMeshProUGUI>();
    }

    /// <summary>Type the current line</summary>
    public void TypeLine(string line, FinishWriting onFinish)
    {
        currentLine = line;
        typeText.text = "";
        OnFinishWriting = onFinish;
        typeWriteCoroutine = StartCoroutine(TypeLineCoroutine());
    }

    /// <summary>Show the whole line before the typewriter is done</summary>
    public void PreemptiveLineFinish()
    {
        if (isTypeCoroutineActive)
        {
            StopCoroutine(typeWriteCoroutine);
        }
        typeText.text = currentLine;
        FinishTyping();
    }

    /// <summary>Coroutine to make the letters appear slowly</summary>
    private IEnumerator TypeLineCoroutine()
    {
        isTypeCoroutineActive = true;
        // Individually write each character
        for (int i = 0; i < currentLine.Length; ++i)
        {
            char c = currentLine[i];
            // If the next character is a space, check if the next word would make us start a new line.
            if (c == ' ')
            {
                // Get the next word
                string nextWord = GetNextWordFromCurrentLine(i);
                // Start the next line if the word would make us start a new line
                if (WillNextWordStartNewLine(nextWord))
                {
                    TypeOneLetter('\n');
                }
                // Otherwise, type the space normally
                else
                {
                    TypeOneLetter(c);
                }
            }
            // Otherwise, type the next letter normally
            else
            {
                TypeOneLetter(c);
            }
            
            dialogue_sfx.Play();
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        // Finish Typing.
        FinishTyping();
        isTypeCoroutineActive = false;
        yield return null;
    }

    /// <summary>Gets the next word after the start index from the current line.
    /// Only works if the index is the index of the first letter of the next word or a space.</summary>
    /// <param name="startIndex">Pulls the first word after this index.</param>
    /// <returns>string. Next word from the line.</returns>
    private string GetNextWordFromCurrentLine(int startIndex)
    {
        string rtnStr = "";
        int curIndex = startIndex;
        // Skip over any spaces at the start
        while (curIndex < currentLine.Length && currentLine[curIndex] == ' ')
        {
            ++curIndex;
        }
        // Append the characters until we reach a spaces
        while (curIndex < currentLine.Length && currentLine[curIndex] != ' ')
        {
            rtnStr += currentLine[curIndex];
            ++curIndex;
        }
        return rtnStr;
    }

    /// <summary>Returns true if the given word will cause a new line to start. False otherwise.</summary>
    /// <param name="nextWord">Next word that might cause a new line to start.</param>
    /// <returns>bool - True if the next word will cause a new line. False otherwise.</returns>
    private bool WillNextWordStartNewLine(string nextWord)
    {
        // Get the amount of lines and text before we add anything to it
        int startLineAmount = typeText.textInfo.lineCount;
        string curText = typeText.text;
        // Add the next word to the text (and a w for good measure)
        typeText.text += nextWord + "w";
        // Force the mesh update to reflect the word append in the line count
        typeText.ForceMeshUpdate();
        int newLineAmount = typeText.textInfo.lineCount;
        // Reset the text to what it was before
        typeText.text = curText;

        // If a new line was created to hold the new word, return true
        return newLineAmount > startLineAmount;
    }

    /// <summary>Appends the given letter to the text</summary>
    /// <param name="c">Character to append</param>
    private void TypeOneLetter(char c)
    {
        typeText.text += c;
    }

    /// <summary>Calls the OnFinishWriting delegate and discards it</summary>
    private void FinishTyping()
    {
        OnFinishWriting?.Invoke();
        OnFinishWriting = null;
    }
}
