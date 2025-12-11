using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    [SerializeField] private float restartTimer = 3f;
    [SerializeField] private InputActionReference tooglePause;
    [SerializeField] private GameObject shopButton;
    private bool isPaused = false;  
    
    private void Start()
    {

        if(tooglePause is not null) tooglePause.action.performed += TogglePuase;
    }

    public void ToggleShop()
    {
        if (ShopSystem.Instance.IsShopOpen())
        {

            ShopSystem.Instance.CloseShop();
            shopButton.SetActive(true);
            SetPause(false);
        }
        else
        {
            ShopSystem.Instance.OpenShop();
            shopButton.SetActive(false);
            SetPause(true);
        }

    }

    private void TogglePuase(InputAction.CallbackContext context)
    {
        if (!ShopSystem.Instance.IsShopOpen())
        {
            TogglePuase();
        }
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

    public void TogglePuase()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void SetPause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void CloseShop()
    {
        ShopSystem.Instance.CloseShop();
        shopButton.SetActive(true);
        SetPause(false);
    }

    public bool IsPaused => isPaused;
}
