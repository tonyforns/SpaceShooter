using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float coolDownTime;
    [SerializeField] private string shooterTag;

    private float coolDownTimer;
    private ObjectPool<IBullet> bulletPool;

    private void OnEnable()
    {
        coolDownTimer = coolDownTime;
        bulletPool = new ObjectPool<IBullet>(
           createFunc: CreateBullet,
           actionOnGet: OnGetBullet,
           actionOnRelease: OnReleaseBullet,
           actionOnDestroy: OnDestroyBullet,
           collectionCheck: true,
           defaultCapacity: 10,
           maxSize: 100
       );
    }
   

    void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }
    public int BulletsCount()
    {
        return 0;
    }

    public bool CanFire()
    {
        return coolDownTimer <= 0;
    }

    public void Fire()
    {
        if (CanFire())
        {
            IBullet bullet = bulletPool.Get();
            bullet.Fire(firePoint.transform);
            coolDownTimer = coolDownTime;
        }
    }

    private IBullet CreateBullet()
    {
        IBullet newBullet = Instantiate(bulletPrefab).GetComponent<IBullet>();
        newBullet.Init(bulletPool, shooterTag);
        return newBullet;
    }
    private void OnGetBullet(IBullet bullet)
    {
    }

    private void OnReleaseBullet(IBullet bullet)
    {
        if (bullet is MonoBehaviour bulletMono)
        {
            bulletMono.gameObject.SetActive(false);
        }
    }

    private void OnDestroyBullet(IBullet bullet)
    {
        if (bullet is MonoBehaviour bulletMono)
        {
            Destroy(bulletMono.gameObject);
        }
    }

    public void Init(string tag)
    {
        shooterTag = tag;
    }
}
