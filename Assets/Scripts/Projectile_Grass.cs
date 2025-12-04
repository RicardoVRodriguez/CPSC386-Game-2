using UnityEngine;
//Upon triggering with the enemy, they are destroyed along with the enemy they impact.
public class Projectile_Grass : MonoBehaviour
{
    public float lifetime = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster-Mushroom"))
        {
            other.gameObject.GetComponent<Monster_Mushroom>().MoveSpeed -= 1;
        }
        else if (other.gameObject.CompareTag("Monster-Meat"))
        {
                other.gameObject.GetComponent<Monster_Meat>().MoveSpeed -= 2;
        }
        else if (other.gameObject.CompareTag("Monster-Cabbage"))
        {
            other.gameObject.GetComponent<Monster_Cabbage>().MoveSpeed -= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster-Mushroom"))
        {
            other.gameObject.GetComponent<Monster_Mushroom>().MoveSpeed += 1;
        }
        else if (other.gameObject.CompareTag("Monster-Meat"))
        {
            other.gameObject.GetComponent<Monster_Meat>().MoveSpeed += 2;
        }
        else if (other.gameObject.CompareTag("Monster-Cabbage"))
        {
            other.gameObject.GetComponent<Monster_Cabbage>().MoveSpeed += 2;
        }
    }
}
