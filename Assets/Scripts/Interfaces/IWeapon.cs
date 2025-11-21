
using UnityEngine;

public interface IWeapon 
{
    void Fire();
    bool CanFire();
    int BulletsCount();
    }
