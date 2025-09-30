using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public GameObject projectile;
    // player movement
    public float speed = 0;
    public float moveVertical = 0.0f;
    public float moveHorizontal = 0.0f;
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

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject clone = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().AddForce(transform.right * 500);
            //Instantiate(projectile, transform.position, transform.rotation);
        }
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
}
