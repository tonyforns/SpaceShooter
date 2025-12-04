using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager>
{
    [Header("UI References")]
    [SerializeField] private Canvas transitionCanvas;
    [SerializeField] private Image fadePanel;
    [SerializeField] private Image warpEffect;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform rightPanel;
    [SerializeField] private RectTransform starsContainer;
    
    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 2f;
    [SerializeField] private float warpSpeed = 5f;
    [SerializeField] private AnimationCurve warpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Star Effect Settings")]
    [SerializeField] private int numberOfStars = 50;
    [SerializeField] private float starSpeed = 200f;
    
    private bool isTransitioning = false;
    private List<RectTransform> stars = new List<RectTransform>();

    private new void Awake()
    {
        base.Awake();
        SetupTransitionElements();
    }

    private void SetupTransitionElements()
    {
        if (transitionCanvas == null)
        {
            CreateTransitionCanvas();
        }
        
        // Ensure transition canvas is always on top
        transitionCanvas.sortingOrder = 1000;
        transitionCanvas.gameObject.SetActive(false);
    }

    private void CreateTransitionCanvas()
    {
        GameObject canvasGO = new GameObject("TransitionCanvas");
        transitionCanvas = canvasGO.AddComponent<Canvas>();
        transitionCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        transitionCanvas.sortingOrder = 1000;
        
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        canvasGO.AddComponent<GraphicRaycaster>();
        
        CreateTransitionElements();
    }

    private void CreateTransitionElements()
    {
        // Create fade panel
        GameObject fadePanelGO = new GameObject("FadePanel");
        fadePanelGO.transform.SetParent(transitionCanvas.transform);
        fadePanel = fadePanelGO.AddComponent<Image>();
        fadePanel.color = new Color(0, 0, 0, 0);
        fadePanel.rectTransform.anchorMin = Vector2.zero;
        fadePanel.rectTransform.anchorMax = Vector2.one;
        fadePanel.rectTransform.sizeDelta = Vector2.zero;
        fadePanel.rectTransform.anchoredPosition = Vector2.zero;

        // Create warp effect
        GameObject warpGO = new GameObject("WarpEffect");
        warpGO.transform.SetParent(transitionCanvas.transform);
        warpEffect = warpGO.AddComponent<Image>();
        warpEffect.color = new Color(1, 1, 1, 0);
        warpEffect.rectTransform.anchorMin = Vector2.zero;
        warpEffect.rectTransform.anchorMax = Vector2.one;
        warpEffect.rectTransform.sizeDelta = Vector2.zero;
        warpEffect.rectTransform.anchoredPosition = Vector2.zero;

        // Create split panels
        CreateSplitPanels();
        
        // Create UI star effect (replaces particle system)
        CreateStarEffect();
    }

    private void CreateSplitPanels()
    {
        // Left panel
        GameObject leftGO = new GameObject("LeftPanel");
        leftGO.transform.SetParent(transitionCanvas.transform);
        leftPanel = leftGO.AddComponent<RectTransform>();
        Image leftImage = leftGO.AddComponent<Image>();
        leftImage.color = Color.black;
        
        leftPanel.anchorMin = new Vector2(0, 0);
        leftPanel.anchorMax = new Vector2(0.5f, 1);
        leftPanel.sizeDelta = Vector2.zero;
        leftPanel.anchoredPosition = new Vector2(-960, 0); // Start off-screen
        
        // Right panel
        GameObject rightGO = new GameObject("RightPanel");
        rightGO.transform.SetParent(transitionCanvas.transform);
        rightPanel = rightGO.AddComponent<RectTransform>();
        Image rightImage = rightGO.AddComponent<Image>();
        rightImage.color = Color.black;
        
        rightPanel.anchorMin = new Vector2(0.5f, 0);
        rightPanel.anchorMax = new Vector2(1, 1);
        rightPanel.sizeDelta = Vector2.zero;
        rightPanel.anchoredPosition = new Vector2(960, 0); // Start off-screen
    }

    private void CreateStarEffect()
    {
        // Create container for stars
        GameObject starsGO = new GameObject("StarsContainer");
        starsGO.transform.SetParent(transitionCanvas.transform);
        starsContainer = starsGO.AddComponent<RectTransform>();
        
        starsContainer.anchorMin = Vector2.zero;
        starsContainer.anchorMax = Vector2.one;
        starsContainer.sizeDelta = Vector2.zero;
        starsContainer.anchoredPosition = Vector2.zero;
        
        // Create individual stars
        for (int i = 0; i < numberOfStars; i++)
        {
            CreateStar();
        }
    }

    private void CreateStar()
    {
        GameObject starGO = new GameObject("Star");
        starGO.transform.SetParent(starsContainer.transform);
        
        RectTransform starRect = starGO.AddComponent<RectTransform>();
        Image starImage = starGO.AddComponent<Image>();
        
        // Create a simple white square as star
        starImage.color = Color.white;
        
        // Random size
        float size = UnityEngine.Random.Range(2f, 6f);
        starRect.sizeDelta = new Vector2(size, size);
        
        // Random position across screen
        float x = UnityEngine.Random.Range(-960f, 960f);
        float y = UnityEngine.Random.Range(-540f, 540f);
        starRect.anchoredPosition = new Vector2(x, y);
        
        // Random starting alpha
        starImage.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.3f, 1f));
        
        stars.Add(starRect);
        starGO.SetActive(false);
    }

    public void TransitionToScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToSceneCoroutine(sceneName));
        }
    }

    public void TransitionToScene(int sceneIndex)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToSceneCoroutine(sceneIndex));
        }
    }

    private IEnumerator TransitionToSceneCoroutine(object scene)
    {
        isTransitioning = true;
        transitionCanvas.gameObject.SetActive(true);

        // Phase 1: Warp effect and star animation
        yield return StartCoroutine(WarpOutEffect());

        // Phase 2: Split screen effect
        yield return StartCoroutine(SplitScreenEffect());

        // Load new scene
        if (scene is string)
            SceneManager.LoadScene((string)scene);
        else if (scene is int)
            SceneManager.LoadScene((int)scene);

        // Phase 3: Warp in effect
        yield return StartCoroutine(WarpInEffect());

        // Phase 4: Split screen close
        yield return StartCoroutine(SplitScreenCloseEffect());

        transitionCanvas.gameObject.SetActive(false);
        isTransitioning = false;
    }

    private IEnumerator WarpOutEffect()
    {
        // Activate stars
        foreach (var star in stars)
        {
            star.gameObject.SetActive(true);
            // Reset position for animation
            float x = UnityEngine.Random.Range(-1200f, 1200f);
            float y = UnityEngine.Random.Range(-700f, 700f);
            star.anchoredPosition = new Vector2(x, y);
        }
        
        float elapsed = 0f;
        float duration = transitionDuration * 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            // Warp effect
            float warpAlpha = warpCurve.Evaluate(progress) * 0.8f;
            warpEffect.color = new Color(1, 1, 1, warpAlpha);
            
            // Fade effect
            float fadeAlpha = fadeCurve.Evaluate(progress) * 0.5f;
            fadePanel.color = new Color(0, 0, 0, fadeAlpha);
            
            // Animate stars
            AnimateStars(progress);
            
            yield return null;
        }
    }

    private void AnimateStars(float progress)
    {
        foreach (var star in stars)
        {
            if (star == null) continue;
            
            // Move stars toward center creating tunnel effect
            Vector2 currentPos = star.anchoredPosition;
            Vector2 direction = (Vector2.zero - currentPos).normalized;
            
            float speed = starSpeed * (1f + progress * 3f); // Accelerate over time
            star.anchoredPosition += direction * speed * Time.deltaTime;
            
            // Scale effect
            float distance = Vector2.Distance(currentPos, Vector2.zero);
            float scale = Mathf.Lerp(0.5f, 2f, progress);
            star.localScale = Vector3.one * scale;
            
            // Alpha effect based on distance and progress
            Image starImage = star.GetComponent<Image>();
            if (starImage != null)
            {
                float alpha = Mathf.Lerp(0.3f, 1f, 1f - distance / 1000f) * (1f - progress * 0.5f);
                Color currentColor = starImage.color;
                
                // Color shift for warp effect
                Color warpColor = Color.Lerp(Color.white, Color.cyan, progress);
                starImage.color = new Color(warpColor.r, warpColor.g, warpColor.b, alpha);
            }
            
            // Reset star if too close to center
            if (distance < 50f)
            {
                float x = UnityEngine.Random.Range(-1200f, 1200f);
                float y = UnityEngine.Random.Range(-700f, 700f);
                star.anchoredPosition = new Vector2(x, y);
            }
        }
    }

    private IEnumerator SplitScreenEffect()
    {
        float elapsed = 0f;
        float duration = transitionDuration * 0.2f;

        Vector2 leftStart = new Vector2(-960, 0);
        Vector2 leftEnd = new Vector2(0, 0);
        Vector2 rightStart = new Vector2(960, 0);
        Vector2 rightEnd = new Vector2(0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            float easedProgress = Mathf.SmoothStep(0, 1, progress);
            
            leftPanel.anchoredPosition = Vector2.Lerp(leftStart, leftEnd, easedProgress);
            rightPanel.anchoredPosition = Vector2.Lerp(rightStart, rightEnd, easedProgress);
            
            // Full fade during split
            fadePanel.color = Color.black;
            
            yield return null;
        }

        leftPanel.anchoredPosition = leftEnd;
        rightPanel.anchoredPosition = rightEnd;
        
        // Hide stars during scene change
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }
    }

    private IEnumerator WarpInEffect()
    {
        float elapsed = 0f;
        float duration = transitionDuration * 0.3f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = 1f - (elapsed / duration);
            
            // Reverse warp effect
            float warpAlpha = warpCurve.Evaluate(progress) * 0.6f;
            warpEffect.color = new Color(0.8f, 0.9f, 1f, warpAlpha);
            
            // Fade in
            float fadeAlpha = progress * 0.8f;
            fadePanel.color = new Color(0, 0, 0, fadeAlpha);
            
            yield return null;
        }

        warpEffect.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator SplitScreenCloseEffect()
    {
        float elapsed = 0f;
        float duration = transitionDuration * 0.2f;

        Vector2 leftStart = new Vector2(0, 0);
        Vector2 leftEnd = new Vector2(-960, 0);
        Vector2 rightStart = new Vector2(0, 0);
        Vector2 rightEnd = new Vector2(960, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            float easedProgress = Mathf.SmoothStep(0, 1, progress);
            
            leftPanel.anchoredPosition = Vector2.Lerp(leftStart, leftEnd, easedProgress);
            rightPanel.anchoredPosition = Vector2.Lerp(rightStart, rightEnd, easedProgress);
            
            // Fade out
            float fadeAlpha = Mathf.Lerp(1f, 0f, progress);
            fadePanel.color = new Color(0, 0, 0, fadeAlpha);
            
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, 0);
        
        // Ensure all stars are hidden
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }
        }

    public void SetTransitionDuration(float duration)
    {
        transitionDuration = duration;
    }

    public bool IsTransitioning()
    {
        return isTransitioning;
    }
}