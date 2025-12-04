using UnityEngine;

public class HealthUp : MonoBehaviour, IConsumible
{
    [SerializeField] private int healthAmount = 1;
    public void Consume(Player player, bool destroy = true)
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.Heal, player.transform.position);
        player.GetLife().IncreaseBaseLife(healthAmount);
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
