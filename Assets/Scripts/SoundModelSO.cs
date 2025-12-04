using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundModelSO", menuName = "Scriptable Objects/SoundModelSO")]
[Serializable]
public class SoundModelSO : ScriptableObject
{
    public enum SoundName
    {
        ButtonClick,
        Explotion,
        BackgroundMusic,
        Fire,
        ShieldUp,
        ShieldDown,
        Heal,
        BuyPowerUp,
        Hit,
        BulletHit,
        PlayerExplotion,
        ShipCrush
            
    }
    
    public SoundName Name;
    public AudioClip Clip;
}
