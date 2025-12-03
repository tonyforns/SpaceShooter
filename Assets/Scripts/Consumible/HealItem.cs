using UnityEngine;

public class HealItem : MonoBehaviour, IConsumible
{
    [SerializeField] private int healAmount = 1;

    public void Consume(Player player)
    {
        Heal(player.GetComponent<Life>());

        Destroy(gameObject);
    }

    public void Heal(Life life)
    {
        life.Heal(healAmount);
    }

}
