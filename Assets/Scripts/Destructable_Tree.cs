using UnityEngine;
// This is currently not in use and was for spawning logs. 
public class Destructable_Tree : MonoBehaviour
{
    public GameObject pickupWood;
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

            Instantiate(pickupWood, transform.position, Quaternion.identity);
           // Destroy(other.gameObject);
            Destroy(gameObject);

        }


    }
}
