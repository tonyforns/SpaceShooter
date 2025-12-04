using UnityEngine;

public class ExtraHelp : MonoBehaviour, IConsumible
{
    [SerializeField] private ExtraHelpWeapon extraHelpWeapon;
    public void Consume(Player player, bool destroy = true)
    {
        player.ChangeWeapon(extraHelpWeapon);

        Destroy(gameObject);
    }
}
