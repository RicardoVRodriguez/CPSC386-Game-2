using UnityEngine;

public class Monster_Cabbage : MonoBehaviour
{
    public GameObject pickupCabbage;
    public GameObject pickupExp;
    private Transform player;
    private Game_Manager gameManager;
    public AudioClip deathSound;
    public int MoveSpeed = 2;
    int MaxDist = 10;
    int MinDist = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("Game-Manager").GetComponent<Game_Manager>();
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


            AudioSource.PlayClipAtPoint(deathSound, transform.position);


            float offset = 0.75f;
            Vector3 pos = new(transform.position.x + offset, transform.position.y + offset, transform.position.z);

            Instantiate(pickupExp, pos, Quaternion.identity);
            Instantiate(pickupCabbage, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);

        }


    }
}
