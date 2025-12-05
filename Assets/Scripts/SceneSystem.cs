using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    [SerializeField] private float restartTimer = 3f;

    private void Start()
    {
    }
    public void RestartSceneAfterDelay()
    {
        StartCoroutine(RestartSceneCoroutine());
    }

    private IEnumerator RestartSceneCoroutine()
    {
        yield return new WaitForSeconds(restartTimer);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {

        SceneManager.LoadScene(sceneIndex);
    }
}
