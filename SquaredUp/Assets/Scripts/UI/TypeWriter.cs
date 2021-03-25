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
    private TextMeshProUGUI typeText;

    // Curent line being written
    private string currentLine;
    // Event to call when finished typing
    public delegate void FinishWriting();
    private FinishWriting OnFinishWriting;

    // Reference to running coroutine
    private Coroutine typeWriteCoroutine;

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
        StopCoroutine(typeWriteCoroutine);
        typeText.text = currentLine;
        FinishTyping();
    }

    /// <summary>Coroutine to make the letters appear slowly</summary>
    private IEnumerator TypeLineCoroutine()
    {
        // Indiivually write each character
        for (int i = 0; i < currentLine.Length; ++i)
        {
            char c = currentLine[i];
            //HandleIfNextCharacterIsSpace(c, i);
            TypeOneLetter(c);
            dialogue_sfx.Play();
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        // Finish Typing.
        FinishTyping();
        yield return null;
    }

    /// <summary>If the next character is space, it checks if the next word is too long to be on the current line.
    /// If the word is too long, it starts the next line.</summary>
    /// <param name="c">Next character of the current line to test if its a space.</param>
    /// <param name="index"></param>
    private void HandleIfNextCharacterIsSpace(char c, int index)
    {
        if (c == ' ')
        {
            // Get the next word
            string nextWord = GetNextWordFromCurrentLine(index);
            // Start the next line if the word would make us start a new line
            StartNextLineIfNextWordTooLong(nextWord);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    private string GetNextWordFromCurrentLine(int startIndex)
    {
        string rtnStr = "";
        int curIndex = startIndex;
        while (curIndex < currentLine.Length && currentLine[curIndex] == ' ')
        {
            ++curIndex;
        }
        while (curIndex < currentLine.Length && currentLine[curIndex] != ' ')
        {
            rtnStr += currentLine[curIndex];
            ++curIndex;
        }
        return rtnStr;
    }

    private void StartNextLineIfNextWordTooLong(string nextWord)
    {
        string curText = typeText.text;
        typeText.text = nextWord;
        if (typeText.bounds.size.x > maxSentenceSize)
        {
            typeText.text = curText + "\n";
        }
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
