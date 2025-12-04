using Unity.VisualScripting;
using UnityEngine;
// This script handles instantiating a log pickup upon attack and features a timer for the tree's regrowth. 

public class Tree_Spawn : MonoBehaviour
{
    public int timer = 300;
    public int currentTime = 300;
    public SpriteRenderer spriteRenderer;
    public Sprite tree;
    public Sprite stump;
    public GameObject pickupLogs;
    public Game_Manager gameManager;
    public AudioClip deathSound;
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
            timer = 300 - (25 * gameManager.StatsUpgradeArr[0].m_Level);
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
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Instantiate(pickupLogs, pos, Quaternion.identity);
            Destroy(other.gameObject);
        }
       
    }
}
