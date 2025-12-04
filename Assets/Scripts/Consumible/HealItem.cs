using UnityEngine;

public class HealItem : MonoBehaviour, IConsumible
{
    [SerializeField] private int healAmount = 1;

    public void Consume(Player player, bool destroy = true)
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.Heal, player.transform.position);
        Heal(player.GetLife());

        if(destroy) Destroy(gameObject);
    }

    public void Heal(Life life)
    {
        life.Heal(healAmount);
    }

}
