using UnityEngine;

public class ShieldSpeedUp : MonoBehaviour, IConsumible
{
    [SerializeField] private float shieldRechargeReductionPercentage = 0.2f;
    public void Consume(Player player, bool destroy = true)
    {
        player.GetShield().ReduceShieldRechargeTime(shieldRechargeReductionPercentage);

        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
