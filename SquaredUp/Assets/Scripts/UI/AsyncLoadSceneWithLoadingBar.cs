using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

public class AsyncLoadSceneWithLoadingBar : MonoBehaviour
{
    [SerializeField] private LoadingText loadingText = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private TextMeshProUGUI percentText = null;


    private void Start()
    {
        gameObject.SetActive(false);
    }


    public void LoadLevel(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    private IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            loadingText.UpdateLoadingText();
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            string numberText = string.Format("{0:f2}", progress * 100.0f);
            percentText.text = numberText + '%';

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
