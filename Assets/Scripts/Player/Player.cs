using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Life))]
[RequireComponent (typeof(PlayerMovment))]
[RequireComponent (typeof(PlayerAttack))]
public class Player : Singleton<Player>, IHitable
{
    public Action<float> OnLifeChanged;
    public Action<bool> OnShieldChanged;
    public Action OnDeath;

    [SerializeField] private Life life;
    [SerializeField] private Shield shield;
    [SerializeField] private PlayerMovment playerMovment;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private ExplodeAnim explodeAnim;


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
        OnShieldChanged.Invoke(isShieldUp);
    }
    public float GetShieldTimerNormalize()
    {
        return shield.GetRechargeTimeNormaize();
    }
    public void Hit(int damage)
    {
        life.TakeDamage(damage);

        OnLifeChanged?.Invoke(life.GetLifeNormalize());
    }

    private void HandleDeath()
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.PlayerExplotion, transform.position);
        explodeAnim.Explode();
        OnDeath?.Invoke();
        gameObject.SetActive(false);
    }

    private void HandleConsumible(Collider2D collision)
    {
        IConsumible consumible = collision.GetComponent<IConsumible>();
        if (consumible != null)
        {
            consumible.Consume(this);
        }
    }
    public Life GetLife()
    {
        return life;
    }

    public Shield GetShield()
    {
        return shield;
    }

    internal void ChangeWeapon(ExtraHelpWeapon extraHelpWeapon)
    {
        playerAttack.ChangeWeapon(extraHelpWeapon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleConsumible(collision);
    }
}
