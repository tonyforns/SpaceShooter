using UnityEngine;

public interface IConsumible 
{
    void Consume(Player player, bool destroy = true);
}
