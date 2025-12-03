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
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            if (rb != null)
            {
                rb.linearVelocity = transform.right * speed;
            }
        }

        private void FixedUpdate()
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

                hitable.Hit(damage);
                OnHit();
            }
        }

        public void Fire(Transform parentTransform)
        {
            transform.position = parentTransform.position;
            transform.rotation = parentTransform.rotation;

            gameObject.SetActive(true);

            if (rb != null)
            {
                rb.linearVelocity = transform.right * speed;
            }
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
