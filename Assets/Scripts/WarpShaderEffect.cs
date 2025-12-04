using UnityEngine;

public class WarpShaderEffect : MonoBehaviour
{
    [Header("Warp Settings")]
    [SerializeField] private Material warpMaterial;
    [SerializeField] private float warpStrength = 0.1f;
    [SerializeField] private float warpSpeed = 2f;
    [SerializeField] private bool isActive = false;
    
    private Camera targetCamera;
    private float currentWarpTime = 0f;

    private void Awake()
    {
        targetCamera = Camera.main;
        if (targetCamera == null)
            targetCamera = FindFirstObjectByType<Camera>();
    }

    private void Update()
    {
        if (isActive && warpMaterial != null)
        {
            currentWarpTime += Time.deltaTime * warpSpeed;
            warpMaterial.SetFloat("_WarpTime", currentWarpTime);
            warpMaterial.SetFloat("_WarpStrength", warpStrength);
        }
    }

    public void StartWarpEffect(float duration)
    {
        if (warpMaterial != null)
        {
            isActive = true;
            StartCoroutine(WarpEffectCoroutine(duration));
        }
    }

    private System.Collections.IEnumerator WarpEffectCoroutine(float duration)
    {
        float elapsed = 0f;
        float originalStrength = warpStrength;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            
            // Create a wave effect for warp strength
            float waveEffect = Mathf.Sin(progress * Mathf.PI * 4) * (1 - progress);
            warpStrength = originalStrength * waveEffect;
            
            yield return null;
        }

        isActive = false;
        warpStrength = 0f;
    }

    public void SetWarpStrength(float strength)
    {
        warpStrength = strength;
    }

    public void SetWarpSpeed(float speed)
    {
        warpSpeed = speed;
    }
}