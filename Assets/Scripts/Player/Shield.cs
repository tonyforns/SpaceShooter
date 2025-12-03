using System;
using UnityEngine;

public class Shield : MonoBehaviour, IHitable
{
    public Action<bool> OnShieldChange;
    public Action<Collider2D> OnShieldTrigger;

    [SerializeField] private Life shieldLife;
    [SerializeField] private float shieldBaseTime;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider;
    private float timer = 0;


    private void Awake()
    {
        if(shieldLife is null) shieldLife = GetComponent<Life>();
        if(spriteRenderer is null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        shieldLife.OnDeath += ShieldDown;
    }
    private void Update()
    {
        HandleShieldRecharge();
    }

    private void HandleShieldRecharge()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ShieldUp();
            }
        }
    }

    public void ShieldUp()
    {
        shieldLife.ResetLife();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, shieldLife.GetLifeNormalize());
        collider.enabled = true;
        spriteRenderer.enabled = true;
        OnShieldChange?.Invoke(collider.enabled);
    }
    public void ShieldDown()
    {
        collider.enabled = false;
        spriteRenderer.enabled = false;
        timer = shieldBaseTime;
        OnShieldChange?.Invoke(collider.enabled);
    }

    public void Hit(int damage)
    {
        shieldLife.TakeDamage(damage);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, shieldLife.GetLifeNormalize());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnShieldTrigger?.Invoke(collision);
    }

}
