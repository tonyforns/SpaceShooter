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

        private void OnCollisionEnter(Collision collision)
        {
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
            if (Camera.main == null) return;
            float outOfBoundsDistance = 1.1f; 
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

            bool isOutOfScene = screenPoint.x < -outOfBoundsDistance || screenPoint.x > 1 + outOfBoundsDistance ||
                   screenPoint.y < -outOfBoundsDistance || screenPoint.y > 1 + outOfBoundsDistance ||
                   screenPoint.z < 0;

            if(isOutOfScene)
            {
                gameObject.SetActive(false);
                if (bulletPool is not null) bulletPool.Release(this);
            }
        }
    }
}
