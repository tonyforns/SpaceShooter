using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Life))]
[RequireComponent(typeof(ExplodeAnim))]
public class BaseEnemy : MonoBehaviour, IHitable
{
    [SerializeField] internal Life life;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private ExplodeAnim explodeAnim;
    private int crushDamage = 1;
    private ObjectPool<BaseEnemy> enemyPool;

    internal void Awake()
    {
        if(explodeAnim is null) explodeAnim = GetComponent<ExplodeAnim>();
        if (life is null) life = GetComponent<Life>();
    }

    internal void Start()
    {
        life.OnDeath += Die;

    }
    private void OnEnable()
    {
        life.ResetLife();
    }

    internal void Update()
    {
        if(!transform.IsRendering())
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        } 
    }
    public void Hit(int damage)
    {
        life.TakeDamage(damage);
    }

    internal void Die()
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.Explotion, transform.position, true);
        explodeAnim.Explode();
        gameObject.SetActive(false);
        ScoreSystem.Instance.AddScore(scoreValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out var hitable))
        {
            SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.ShipCrush, transform.position, true);
            hitable.Hit(crushDamage);
            life.TakeDamage(crushDamage);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
