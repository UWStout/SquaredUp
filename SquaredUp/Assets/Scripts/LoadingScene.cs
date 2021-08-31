using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private string sceneName = "LoadingScene";

    // Start is called before the first frame update
    private void Awake()
    {
        SceneManager.sceneLoaded += UnloadLoadingScene;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= UnloadLoadingScene;
    }


    private void UnloadLoadingScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name != sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
