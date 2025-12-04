using UnityEngine;
//Upon triggering with the enemy the enemy is destroyed but his object will persist until lifetime is equal to 0
public class Projectile_Poison : MonoBehaviour
{
    public float lifetime = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
