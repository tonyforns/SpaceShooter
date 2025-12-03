using System.Transactions;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Interfaces
{
    public interface IBullet
    {
        void Fire(Transform parentTransform);
        void OnHit();

        void HandleOutOfScene();
    
        void Init(ObjectPool<IBullet> pool, string ignoreTag);
    }
}
