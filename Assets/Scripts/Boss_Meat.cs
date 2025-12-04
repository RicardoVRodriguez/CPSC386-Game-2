using UnityEngine;
using System.Collections;
// This script handles the gameplay logic for the Boss-Meat. this script is responsible for
// attacking the player and keeping track of the boss' health. 
// This script is for handeling enemy logic for following the player, handle item drops, play a sound upon death, and damage the player. 
public class Boss_Meat : MonoBehaviour
{
    public int health = 10;
    public GameObject pickup;
    public AudioClip deathSound;
    public float moveSpeed;
    bool spawnContinuously = true;
    public float timeBetweenAttacks = 3.0f;
    int randomnumber = 0;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (spawnContinuously)
        {
            randomnumber = Random.Range(0, 4);

            yield return new WaitForSeconds(timeBetweenAttacks);
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
        if (randomnumber == 0)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        else if (randomnumber == 1)
        {
            transform.position += -transform.up * moveSpeed * Time.deltaTime;
        }
        else if (randomnumber == 2)
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }
        else if (randomnumber == 3)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
        }

    }
}
