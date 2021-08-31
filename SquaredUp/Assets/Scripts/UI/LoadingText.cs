using UnityEngine;
using UnityEngine.UI;

using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LoadingText : MonoBehaviour
{
    [SerializeField] private int maxPeriods = 3;
    [SerializeField] private string baseText = "Loading";
    [SerializeField] [Min(0.0f)] private float waitBetweenPeriods = 1.0f;
    [SerializeField] private Slider loadingBar = null;

    private TextMeshProUGUI loadingText = null;

    private int curPeriods = 0;
    private float curWaitTime = 0.0f;


    // Called 0th
    // Domestic Initializatioon
    private void Awake()
    {
        loadingText = GetComponent<TextMeshProUGUI>();
    }
    // Called 1st
    // Foreign Initialization
    private void Start()
    {
        loadingText.text = baseText;
    }


    public void UpdateLoadingText()
    {
        if (curWaitTime < waitBetweenPeriods)
        {
            curWaitTime += Time.deltaTime;
            return;
        }
        curWaitTime = 0.0f;

        if (curPeriods < maxPeriods)
        {
            ++curPeriods;
            loadingText.text += '.';
        }
        else
        {
            curPeriods = 0;
            loadingText.text = baseText;
        }
    }
}
