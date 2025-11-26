using UnityEngine;

public class EnemyForwardMovment : BaseEnemyMovment
{
    internal override void Move()
    {
       transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
