using UnityEngine;
//Upon triggering with the enemy, they are destroyed along with the enemy they impact.
//They may instantiate another object that can do damage after they are destroyed,

public class Projectile_Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float lifetime = 1.0f;
    public GameObject poison;
    public GameObject poisonGroup;
    public GameObject grass;
    public GameObject grassGroup;
    GameObject player;
    private Vector3 scaleChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null && player.GetComponent<Player>().meatLevel >= 2)
        {
            scaleChange = new Vector3(1.05f, 1.05f, 1.0f);
            gameObject.transform.localScale += scaleChange;
        }

        if (player.GetComponent<Player>().meatLevel >= 4)
        {
            lifetime = 4.0f;
        }
        else if (player.GetComponent<Player>().meatLevel >= 5)
        {
            lifetime = 5.0f;
        }



        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        
    }

    void OnDestroy()
    {
        
        if (player != null && player.GetComponent<Player>().mushroomLevel >= 4)
        {
            Instantiate(poisonGroup, transform.position, Quaternion.identity);
        }
        else if (player != null && player.GetComponent<Player>().mushroomLevel >= 2)
        {
            Instantiate(poison, transform.position, Quaternion.identity);
        }

        if (player != null && player.GetComponent<Player>().cabbageLevel >= 4)
        {
            Instantiate(grassGroup, transform.position, Quaternion.identity);
        }
        else if (player != null && player.GetComponent<Player>().cabbageLevel >= 2)
        {
            Instantiate(grass, transform.position, Quaternion.identity);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster-Mushroom"))
        {
            Destroy(other.gameObject);
            if (player.GetComponent<Player>().meatLevel < 3)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("Monster-Meat"))
        {
            Destroy(other.gameObject);
            if (player.GetComponent<Player>().meatLevel < 3)
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("Monster-Cabbage"))
        {
            Destroy(other.gameObject);
            if (player.GetComponent<Player>().meatLevel < 3)
            {
                Destroy(gameObject);
            }
        }

       

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
