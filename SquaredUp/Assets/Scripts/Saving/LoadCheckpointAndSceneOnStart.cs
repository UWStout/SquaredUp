using System.Collections;
using UnityEngine;

using NaughtyAttributes;

public class LoadCheckpointAndSceneOnStart : MonoBehaviour
{
    [SerializeField] [Scene] private string m_sceneName = "SquaredUp_Combined";
    [SerializeField] private AsyncLoadSceneWithLoadingBar m_asyncLoadWithBar = null;


    private void Start()
    {
        StartCoroutine(LoadCheckpointAfterFrame());
    }


    private IEnumerator LoadCheckpointAfterFrame()
    {
        yield return null;

        LoadSaveOnStart.s_currentLoadOption = LoadSaveOnStart.eLoadOption.CHECKPOINT;
        m_asyncLoadWithBar.LoadLevel(m_sceneName);
    }
}
