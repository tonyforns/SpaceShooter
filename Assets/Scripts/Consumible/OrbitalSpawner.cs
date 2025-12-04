using UnityEngine;

public class OrbitalSpawner : MonoBehaviour, IConsumible
{
    [SerializeField] private GameObject orbitalPrefab;
    public void Consume(Player player, bool destroy = true)
    {
        SoundSystem.Instance.PlaySound(SoundModelSO.SoundName.BuyPowerUp, player.transform.position);
        GameObject orbital = Instantiate(orbitalPrefab);
        Debug.Log("Orbital spawned");
        orbital.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        orbital.transform.parent = player.transform;
        orbital.transform.localPosition = Vector3.zero;

        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
