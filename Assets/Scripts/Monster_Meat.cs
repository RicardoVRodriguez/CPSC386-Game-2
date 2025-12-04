using UnityEngine;
// This script is for handeling enemy logic for following the player, handle item drops, play a sound upon death, and damage the player. 
public class Monster_Meat : MonoBehaviour
{
    public GameObject pickupMeat;
    public GameObject pickupExp;
    private Transform player;
    public Game_Manager gameManager;
    public AudioClip deathSound;
    public int MoveSpeed = 3;
    private int expChance = 5;
    private int pickupChance = 9;
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
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            float offset = 0.75f;
            Vector3 pos = new(transform.position.x + offset, transform.position.y + offset, transform.position.z);

            


            int randomNum = Random.Range(1, 11);

            if (randomNum >= expChance)
            {
                Instantiate(pickupExp, pos, Quaternion.identity);
            }

            if (randomNum >= pickupChance - gameManager.StatsUpgradeArr[2].m_Level)
            {
                Instantiate(pickupMeat, transform.position, Quaternion.identity);
            }


        }


    }
}
