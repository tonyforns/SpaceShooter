using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    [SerializeField] private float restartTimer = 3f;
    [SerializeField] private bool useTransitionEffects = true;


    private void Start()
    {
        SoundSystem.Instance.PlayEnviromentMusic();
    }
    public void RestartSceneAfterDelay()
    {
        StartCoroutine(RestartSceneCoroutine());
    }

    private IEnumerator RestartSceneCoroutine()
    {
        yield return new WaitForSeconds(restartTimer);

        if (useTransitionEffects && SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.TransitionToScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (useTransitionEffects && SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.TransitionToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if (useTransitionEffects && SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.TransitionToScene(sceneIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void SetUseTransitionEffects(bool useEffects)
    {
        useTransitionEffects = useEffects;
    }
}
