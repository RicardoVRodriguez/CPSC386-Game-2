using UnityEngine;

public class Monster_Mushroom : MonoBehaviour
{

    public GameObject pickupMushroom;
    public GameObject pickupExp;
    private Transform player;
    int MoveSpeed = 1;
    int MaxDist = 10;
    int MinDist = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector2.Distance(transform.position, player.position) >= MinDist)
        {
            // First, make the enemy face the player (2D)
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * MoveSpeed * Time.deltaTime;

            if (Vector2.Distance(transform.position, player.position) <= MaxDist)
            {
                // Shoot or other action
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Projectile"))
        {
            float offset = 0.75f;
            Vector3 pos = new(transform.position.x + offset, transform.position.y + offset, transform.position.z);

            Instantiate(pickupExp, pos, Quaternion.identity);

            Instantiate(pickupMushroom, transform.position, Quaternion.identity);
            
            
            Destroy(gameObject);
            
        }

        
    }
}
