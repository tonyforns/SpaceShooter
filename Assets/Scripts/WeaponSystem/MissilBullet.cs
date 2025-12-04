using Assets.Scripts;
using UnityEngine;

public class MissilBullet : BaseBullet
{
    [SerializeField] private float minFollowDistance = 3f;
    [SerializeField] private Transform target;
    [SerializeField] private bool followTargert;
    public void SetTarget(Transform target)
    {
        this.target = target;
        followTargert = true;
    }

    internal new void Update()
    {
        base.Update();

        if (followTargert)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (Vector2.Distance(transform.position, target.position) < minFollowDistance)
            {
                followTargert = false;
            }
        }

    }
}
