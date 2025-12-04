using UnityEngine;

public class ExplodeAnim : MonoBehaviour
{
    [SerializeField] private GameObject explodeEffect;

    public void Explode()
    {
        if (explodeEffect != null)
        {
            Instantiate(explodeEffect, transform.position, Quaternion.identity).SetActive(true);
        }
    }
}
