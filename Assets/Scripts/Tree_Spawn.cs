using UnityEngine;

public class Tree_Spawn : MonoBehaviour
{
    public int timer = 250;
    public int currentTime = 250;
    public SpriteRenderer spriteRenderer;
    public Sprite tree;
    public Sprite stump;
    public GameObject pickupLogs;
    private bool isStump = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (isStump && currentTime > 0)
        {
            currentTime--;
        }
        else if (isStump && currentTime <= 0)
        {
            currentTime = timer;
            spriteRenderer.sprite = tree;
            isStump = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile") && !isStump)
        {
            spriteRenderer.sprite = stump;
            isStump = true;

            float offset = 0.75f;
            Vector3 pos = new(transform.position.x + offset, transform.position.y + offset, transform.position.z);

            Instantiate(pickupLogs, pos, Quaternion.identity);
            Destroy(other.gameObject);
        }
       
    }
}
