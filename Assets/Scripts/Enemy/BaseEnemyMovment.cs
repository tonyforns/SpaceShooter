using UnityEngine;

public abstract class BaseEnemyMovment : MonoBehaviour
{
    [SerializeField] internal float moveSpeed = 5;

    private void Update()
    {
        Move();
    }

    internal abstract void Move();
}
