using System.Collections;

using UnityEngine;
// This script handles the gameplay logic for the Boss-Mushroom. this script is responsible for
// attacking the player and keeping track of the boss' health.
// This script is for handeling enemy logic for following the player, handle item drops, play a sound upon death, and damage the player. 
public class Boss_Mushroom : MonoBehaviour
{
    public int health = 10;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public GameObject pickup;
    bool spawnContinuously = true;
    public float timeBetweenAttacks = 3.5f;
    public float bulletSpeed;
    public AudioClip deathSound;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }
    IEnumerator SpawnProjectiles()
    {
        while (spawnContinuously)
        {
          

            yield return new WaitForSeconds(timeBetweenAttacks);

            GameObject clone = Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(-transform.up * bulletSpeed);



        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            health--;
            StartCoroutine(HurtAnimation());
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            if (health <= 0)
            {
                Instantiate(pickup, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator HurtAnimation()
    {
        animator.SetBool("tookDamage", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("tookDamage", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
