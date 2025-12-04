using UnityEngine;

public class ExtraHelp : MonoBehaviour, IConsumible
{
    [SerializeField] private ExtraHelpWeapon extraHelpWeapon;
    public void Consume(Player player, bool destroy = true)
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.BuyPowerUp, player.transform.position);
        player.ChangeWeapon(extraHelpWeapon);

        Destroy(gameObject);
    }
}
