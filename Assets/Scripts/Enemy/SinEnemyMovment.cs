using UnityEngine;

public class SinEnemyMovment : BaseEnemyMovment
{
    [SerializeField] private float amplitude = 3f;          
    [SerializeField] private float frequency = 2f; 
    
    private float timeOffset;
    private Vector3 startPosition;

    private void Start()
    {
        frequency = Random.Range(frequency -2, frequency + 2);
        startPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    internal override void Move()
    {
        Vector2 mainMovement = -transform.right * moveSpeed * Time.deltaTime;
        
        float sineWave = Mathf.Sin((Time.time + timeOffset) * frequency) * amplitude;
        
        Vector2 perpendicularDirection = GetPerpendicularDirection();
        Vector2 sineMovement = perpendicularDirection * sineWave * Time.deltaTime;
        
        transform.Translate(mainMovement + sineMovement);
    }
    
    private Vector2 GetPerpendicularDirection()
    {
        Vector2 mainDir = -transform.right;
        return new Vector2(-mainDir.y, mainDir.x);
    }
}
