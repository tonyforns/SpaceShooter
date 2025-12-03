using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Life))]
public class BaseEnemy : MonoBehaviour, IHitable
{
    [SerializeField] private Life life;
    [SerializeField] private int scoreValue = 10;
    private ObjectPool<BaseEnemy> enemyPool;

    private void Awake()
    {
        life = GetComponent<Life>();
        life.OnDeath += Die;
    }

    private void OnEnable()
    {
        life.ResetLife();
    }

    private void Update()
    {
        if(!transform.IsRendering())
        {
            gameObject.SetActive(false);
        }
    }
    public void Hit(int damage)
    {
        life.TakeDamage(damage);
    }

    private void Die()
    {
       gameObject.SetActive(false);
       ScoreSystem.Instance.AddScore(scoreValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out var hitable))
        {
            hitable.Hit(1);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
