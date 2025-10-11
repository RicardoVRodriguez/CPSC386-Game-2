using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile_Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    public float lifetime = 1.0f;
    public GameObject poison;
    public GameObject grass;
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
       

        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
        
    }

    void OnDestroy()
    {
        
        if (player != null && player.GetComponent<Player>().mushroomLevel >= 2)
        {
            Instantiate(poison, transform.position, Quaternion.identity);
        }
        if (player != null && player.GetComponent<Player>().cabbageLevel >= 2)
        {
            Instantiate(grass, transform.position, Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
