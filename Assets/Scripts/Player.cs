using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject projectile;
    public GameObject projectile2;
    public Slider slider;

    // player stats
    public float speed = 0;
    public float moveVertical = 0.0f;
    public float moveHorizontal = 0.0f;
    public int currentExp = 0;
    public int nextLevelExp = 100;
    public GameObject projectileSpawnPoint;

    // experience and leveling
    // Mushroom exp tracking
    public int pickupMushroomCount = 0;
    public int cookedMushroomCount = 0;
    public int MushroomLevel = 1;

    // meat exp tracking
    public int pickupMeatCount = 0;
    public int cookedMeatCount = 0;
    public int MeatLevel = 1;

    // pickup wood count
    public int pickupLogsCount = 0;

    // attack timers
    public int timer = 50;
    public int currentTime = 50;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // handles player movement
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        rb2d.linearVelocity = new Vector2(moveHorizontal * speed, moveVertical * speed);


        if (currentTime > 0)
        {
            currentTime--;
        }
        else if (currentTime <= 0)
        {
            currentTime = timer;
            projectile.SetActive(!projectile.activeSelf);
            projectile2.SetActive(!projectile2.activeSelf);
            //  GameObject clone = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            //  clone.GetComponent<Rigidbody2D>().AddForce(transform.right * 50);

        }
        /*
        
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject clone = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(transform.right * 500);
            //Instantiate(projectile, transform.position, transform.rotation);
        }
        */
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Pickup-Meat"))
        {
            pickupMeatCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Mushroom"))
        {
            pickupMushroomCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Logs"))
        {
            pickupLogsCount++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pickup-Exp"))
        {
            currentExp += 10;
            ChangeSlider();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Pot"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                cookedMeatCount += pickupMeatCount;
                pickupMeatCount = 0;

                cookedMushroomCount += pickupMushroomCount;
                pickupMushroomCount = 0;
            }
           
        }
    }


    public void ChangeSlider()
    {
        slider.value = Mathf.Clamp01((float)currentExp / nextLevelExp);
    }
}
