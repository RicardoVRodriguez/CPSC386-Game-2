using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject projectile;
    public Slider slider;
    public TMP_Text potText;
    public GameObject fireProjectile;
    public Game_Manager gameManager;
    public AudioClip hitSound;
    public AudioClip pickupSound;
    public AudioSource audioSource;
    

    // player stats
    public bool spawnContinuously = true;

    public float speed = 0.0f;
    public float timeBetweenAttacks = 5.0f;
    public float moveVertical = 0.0f;
    public float moveHorizontal = 0.0f;

    public int maxHealth = 3;
    public int health = 3;
    public int currentExp = 0;
    public int nextLevelExp = 100;
    public int projectileSpeed = 300;

    public GameObject leftProjectileSpawnPoint;
    public GameObject rightProjectileSpawnPoint;
    public GameObject topProjectileSpawnPoint;
    public GameObject bottomProjectileSpawnPoint;

    // experience and leveling
    // Mushroom exp tracking
    public int pickupMushroomCount = 0;
    public int cookedMushroomCount = 0;
    public int mushroomsNeeded = 10;
    public int mushroomLevel = 1;

    // meat exp tracking
    public int pickupMeatCount = 0;
    public int cookedMeatCount = 0;
    public int meatNeeded = 10;
    public int meatLevel = 1;

    // cabbage exp tracking
    public int pickupCabbageCount = 0;
    public int cookedCabbageCount = 0;
    public int cabbageNeeded = 10;
    public int cabbageLevel = 1;

    // pickup wood count
    public int pickupLogsCount = 0;
    public int placedLogsCount = 0;
    public int logsNeeded = 10;
    public int logsLevel = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnProjectiles());
    }

    // Update is called once per frame
    void Update()
    {
        // handles player movement
        speed = 5.0f + ((float)gameManager.StatsUpgradeArr[5].m_Level / 2);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        rb2d.linearVelocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                potText.text = "You placed items into the pot";
                cookedMeatCount += pickupMeatCount;
                pickupMeatCount = 0;

                if (cookedMeatCount >= meatNeeded)
                {
                    meatLevel++;
                    meatNeeded += 10;
                    mushroomsNeeded += 20;
                    logsNeeded += 20;
                }

                cookedMushroomCount += pickupMushroomCount;
                pickupMushroomCount = 0;

                if (cookedMushroomCount >= mushroomsNeeded)
                {
                    mushroomLevel++;
                    mushroomsNeeded += 10;
                    meatNeeded += 20;
                }

                placedLogsCount += pickupLogsCount;
                pickupLogsCount = 0;

                if (placedLogsCount >= logsNeeded)
                {
                    logsLevel++;
                    logsNeeded += 10;
                    mushroomsNeeded += 20;
                    meatNeeded += 20;
                }

                if (cookedCabbageCount >= cabbageNeeded)
                {
                    cabbageLevel++;
                    cabbageNeeded += 10;
                    mushroomsNeeded += 20;
                    meatNeeded += 20;
                    logsNeeded += 20;
                }

            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
        {
            potText.text = "Press E to put items into the pot";
        }

        if (other.gameObject.CompareTag("Pickup-Meat"))
        {
            audioSource.PlayOneShot(pickupSound, 0.7F);
            pickupMeatCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Mushroom"))
        {
            audioSource.PlayOneShot(pickupSound, 0.7F);
            pickupMushroomCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Logs"))
        {
            audioSource.PlayOneShot(pickupSound, 0.7F);
            pickupLogsCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Exp"))
        {
            audioSource.PlayOneShot(pickupSound, 0.7F);
            currentExp += 10;
            ChangeSlider();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Pickup-Cabbage"))
        {
            audioSource.PlayOneShot(pickupSound, 0.7F);
            pickupCabbageCount++;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Monster-Mushroom"))
        {
            audioSource.PlayOneShot(hitSound, 0.7F);
            health -= 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Monster-Meat"))
        {
            audioSource.PlayOneShot(hitSound, 0.7F);
            health--;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Monster-Cabbage"))
        {
            audioSource.PlayOneShot(hitSound, 0.7F);
            health--;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pot"))
        {
            potText.text = "";
        }
        
    }

    IEnumerator SpawnProjectiles()
    {
        while (spawnContinuously)
        {
            timeBetweenAttacks = 4.5f - ((float)gameManager.StatsUpgradeArr[6].m_Level/2);
            projectileSpeed = 200 + (gameManager.StatsUpgradeArr[7].m_Level * 100);

             yield return new WaitForSeconds(timeBetweenAttacks);

            if (logsLevel >= 2)
            {
                Instantiate(fireProjectile, transform.position, Quaternion.identity);
            }
            GameObject clone = Instantiate(projectile, leftProjectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(-transform.right * projectileSpeed);

            clone = Instantiate(projectile, rightProjectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(transform.right * projectileSpeed);

            clone = Instantiate(projectile, topProjectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);

            clone = Instantiate(projectile, bottomProjectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(-transform.up * projectileSpeed);


            
        }
    }


    public void ChangeSlider()
    {
        slider.value = Mathf.Clamp01((float)currentExp / nextLevelExp);
    }
}
