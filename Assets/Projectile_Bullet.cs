using UnityEngine;

public class Projectile_Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;
    private float lifetime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
