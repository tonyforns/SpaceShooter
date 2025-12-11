using Assets.Scripts.Interfaces;
using UnityEngine;

public class Damager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet")) return;

        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
        {
            SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.BulletHit, transform.position, true);
            hitable.Hit(1);
        } else
        if (collision.gameObject.TryGetComponent<IBullet>(out IBullet bullet))
        {
            SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.BulletHit, transform.position, true);
            Destroy(collision.gameObject);
        }
    }
}
