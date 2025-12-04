using System;
using UnityEngine;

public class Shield : MonoBehaviour, IHitable
{
    public Action<bool> OnShieldChange;
    public Action<Collider2D> OnShieldTrigger;

    [SerializeField] private Life shieldLife;
    [SerializeField] private float shieldBaseRechargeTime = 10;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D collider;
    [SerializeField] private float timer = 0;


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
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.ShieldUp, transform.position);
        shieldLife.ResetLife();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, shieldLife.GetLifeNormalize());
        collider.enabled = true;
        spriteRenderer.enabled = true;
        OnShieldChange?.Invoke(collider.enabled);
        timer = 0;
    }
    public void ShieldDown()
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.ShieldDown, transform.position);
        collider.enabled = false;
        spriteRenderer.enabled = false;
        timer = shieldBaseRechargeTime;
        OnShieldChange?.Invoke(collider.enabled);
    }

    public void Hit(int damage)
    {
        shieldLife.TakeDamage(damage);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, shieldLife.GetLifeNormalize());
    }

    public void ReduceShieldRechargeTime(float porcentage)
    {
        shieldBaseRechargeTime *= (1 - porcentage);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnShieldTrigger?.Invoke(collision);
    }

}
