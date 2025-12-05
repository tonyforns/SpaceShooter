using UnityEngine;

public class BoundWarp : MonoBehaviour
{
    [SerializeField] private bool warpX = false;
    [SerializeField] private bool warpY = false;
    [SerializeField] private float offset = 0.25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Vector3 postion = Player.Instance.transform.position;
            if (warpX)
            {
                postion.x = -postion.x + (postion.x > 0 ? offset : -offset);
            }
            if(warpY)
            {
                postion.y = -postion.y + (postion.y > 0 ?   offset : -offset);
            }

            Player.Instance.transform.position = postion;
        }
    }
}
