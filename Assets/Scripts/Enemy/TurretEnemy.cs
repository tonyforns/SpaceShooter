using System;
using UnityEngine;

public class TurretEnemy : BaseEnemy
{
    [SerializeField] private TurretWeapon weapon;
    [SerializeField] private Transform turretHead;

    [SerializeField] private Transform targetTransform;
    [SerializeField] private bool isFiring = false;


    internal new void Awake()
    {
        base.Awake();

        if(weapon is null) weapon = GetComponent<TurretWeapon>();
        weapon.Init(tag);
    }
    private new void Start()
    {
        life.OnDeath += Die;
    }

    internal new void Update()
    {
        HandleFireEnemy();
    }

    internal new void Die()
    {
        base.Die();
        ScoreSystem.Instance.TurretKilled();
    }

    private void HandleFireEnemy()
    {
        if (targetTransform is null || !isFiring) return;
        Vector2 direction = (targetTransform.position - turretHead.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        turretHead.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (isFiring && weapon.CanFire())
        {
            weapon.Fire();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetTransform = collision.transform;
            StartFiring();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetTransform = null;
            StopFiring();
        }
    }
    private void StartFiring()
    {
        weapon.SetTarget(targetTransform);
        isFiring = true;
    }
    private void StopFiring()
    {
        isFiring = false;
    }
}
