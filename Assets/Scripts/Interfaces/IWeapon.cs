
using UnityEngine;

public interface IWeapon 
{
    void Init(string tag);
    void Fire();
    bool CanFire();
    int BulletsCount();
    }
