using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHelpWeapon : BaseWeapon
{
    [SerializeField] private List<Transform> firePoints;

   public override void Fire()
   {
        foreach (Transform firePoint in firePoints)
        {
            IBullet bullet = bulletPool.Get();
            bullet.Fire(firePoint.transform);
        }
        coolDownTimer = coolDownTime;
    }
}
