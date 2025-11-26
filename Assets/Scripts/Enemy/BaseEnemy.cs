using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Life))]
public class BaseEnemy : MonoBehaviour, IHitable
{
    [SerializeField] private Life life;
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
       //enemyPool.Release(this);
    }
}
