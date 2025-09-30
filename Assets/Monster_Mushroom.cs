using UnityEngine;

public class Monster_Mushroom : MonoBehaviour
{

    public GameObject pickupMushroom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Projectile"))
        {

            Instantiate(pickupMushroom, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            
        }

        
    }
}
