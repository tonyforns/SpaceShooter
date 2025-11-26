using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class BaseBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float speed = 10F;
        private ObjectPool<IBullet> bulletPool;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Bullet collided with " + collision.gameObject.name);
            if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable) )
            {
                hitable.Hit(1);
            }
            OnHit();
        }
        private void Update()
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
            HandleOutOfScene();
        }
        public void Fire(Transform parentTransform)
        {
            transform.position = parentTransform.position;
            transform.rotation = parentTransform.rotation;
            gameObject.SetActive(true);
        }

        public void OnHit()
        {
            gameObject.SetActive(false);

            if(bulletPool is not null) bulletPool.Release(this);
        }

        public void SetBulletPool(ObjectPool<IBullet> pool)
        {
            bulletPool = pool;
        }

        public void HandleOutOfScene()
        {
            if(!transform.IsRendering())
            {
                gameObject.SetActive(false);
                if (bulletPool is not null) bulletPool.Release(this);
            }
        }
    }
}
