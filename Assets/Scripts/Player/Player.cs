using System;
using UnityEngine;

[RequireComponent (typeof(Life))]
[RequireComponent (typeof(PlayerMovment))]
[RequireComponent (typeof(PlayerAttack))]
public class Player : Singleton<Player>, IHitable
{
    public Action<float> OnLifeChanged;

    [SerializeField] private Life life;
    [SerializeField] private Shield shield;
    [SerializeField] private PlayerMovment playerMovment;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Collider2D playerCollider;


    private new void Awake()
    {
        base.Awake();

        life = GetComponent<Life>();
        playerMovment = GetComponent<PlayerMovment>();
        playerAttack = GetComponent<PlayerAttack>();

    }
    private void Start()
    {
        life.OnDeath += HandleDeath;
        life.OnLifeChange = () => { OnLifeChanged?.Invoke(life.GetLifeNormalize()); };
        
        shield.OnShieldChange += HandleShieldChange;
        shield.OnShieldTrigger += HandleConsumible;
    }

    private void HandleShieldChange(bool isShieldUp)
    {
        playerCollider.enabled = !isShieldUp;
    }

    public void Hit(int damage)
    {
        life.TakeDamage(damage);

        OnLifeChanged?.Invoke(life.GetLifeNormalize());
    }

    private void HandleDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleConsumible(collision);
    }

    private void HandleConsumible(Collider2D collision)
    {
        IConsumible consumible = collision.GetComponent<IConsumible>();
        if (consumible != null)
        {
            consumible.Consume(this);
        }
    }
}
