using UnityEngine;

public class HealthUp : MonoBehaviour, IConsumible
{
    [SerializeField] private int healthAmount = 1;
    public void Consume(Player player, bool destroy = true)
    {
        player.GetLife().IncreaseBaseLife(healthAmount);
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
