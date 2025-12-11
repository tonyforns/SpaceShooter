using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSystem : Singleton<SceneSystem>
{
    [SerializeField] private float restartTimer = 3f;
    [SerializeField] private InputActionReference tooglePause;
    [SerializeField] private InputActionReference toogleShop;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopButton;
    private bool isPaused = false;  
    
    private void Start()
    {
        tooglePause.action.performed += TogglePuase;
        toogleShop.action.performed += ToggleShop;
    }

    private void ToggleShop(InputAction.CallbackContext context)
    {
        ToggleShop();
    }

    public void ToggleShop()
    {
        bool isShopOpen = shopPanel.activeSelf;

        if (isShopOpen)
        {

            shopPanel.SetActive(false);
            shopButton.SetActive(true);
            SetPause(false);
        }
        else
        {
            shopPanel.SetActive(true);
            shopButton.SetActive(false);
            SetPause(true);
        }
    }

    private void TogglePuase(InputAction.CallbackContext context)
    {
        if (!shopPanel.activeSelf)
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
        shopPanel.SetActive(false);
        shopButton.SetActive(true);
        SetPause(false);
    }

    public bool IsShopOpen => shopPanel.activeSelf;
    public bool IsPaused => isPaused;
}
