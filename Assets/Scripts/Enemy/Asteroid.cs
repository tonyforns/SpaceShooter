using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Life))]
[RequireComponent(typeof(EnemyForwardMovment))]
public class Asteroids : MonoBehaviour, IHitable
{
    [SerializeField] private Life life;
    [SerializeField] private EnemyForwardMovment enemyForwardMovment;
    [SerializeField] private List<GameObject> asteroidView;

    public void Hit(int damage)
    {
        life.TakeDamage(damage);
    }

    private void Awake()
    {
        if(life is null)life = GetComponent<Life>();
        if(enemyForwardMovment is null) enemyForwardMovment = GetComponent<EnemyForwardMovment>();

        int index = Random.Range(0, asteroidView.Count);
        asteroidView[index].SetActive(true);

        asteroidView[index].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<IHitable>(out var hitable))
        {
            hitable.Hit(200);
        }
    }
}
