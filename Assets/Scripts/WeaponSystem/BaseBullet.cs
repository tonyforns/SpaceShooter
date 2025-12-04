using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class BaseBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private string ignoreTag;
        [SerializeField] private float speed = 10F;
        [SerializeField] private int damage = 1;
        private ObjectPool<IBullet> bulletPool;

        internal void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        internal void FixedUpdate()
        {
            HandleOutOfScene();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(ignoreTag))
            {
                return;
            }

            if (collision.gameObject.TryGetComponent<IHitable>(out var hitable))
            {
                SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.BulletHit, transform.position, true);
                hitable.Hit(damage);
                OnHit();
            }
        }

        public void Fire(Transform parentTransform)
        {
            transform.position = parentTransform.position;
            transform.rotation = Quaternion.Euler(0, 0, parentTransform.eulerAngles.z);

            gameObject.SetActive(true);
        }

        public void OnHit()
        {
            gameObject.SetActive(false);
            try {
                if (bulletPool is not null) bulletPool.Release(this);

            } catch(Exception ex) {
                Debug.Log("Error releasing bullet to pool: " + ex.Message);
            }
        }

        public void Init(ObjectPool<IBullet> pool, string ignoreTag)
        {
            this.ignoreTag = ignoreTag;
            bulletPool = pool;
        }

        public void HandleOutOfScene()
        {
            if (!transform.IsRendering())
            {
                gameObject.SetActive(false);
                if (bulletPool is not null) bulletPool.Release(this);
            }
        }
    }
}
