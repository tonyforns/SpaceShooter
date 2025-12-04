using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class TurretWeapon : BaseWeapon
{
    private Transform targetTransform;

    internal void SetTarget(Transform targetTransform)
    {
       this.targetTransform = targetTransform;
    }

    public new void Fire()
    {
        if (CanFire())
        {
            IBullet bullet = bulletPool.Get();
            MissilBullet misilBullet = bullet as MissilBullet;
            misilBullet.SetTarget(targetTransform);
            bullet.Fire(firePoint.transform);
            coolDownTimer = coolDownTime;
        }
    }
}
