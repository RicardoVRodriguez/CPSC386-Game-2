using System.Threading;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public int timer = 250;
    public int currentTime = 250;
    public GameObject monster_Mushroom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime--;
        }
        else if (currentTime <= 0)
        {
            currentTime = timer;
            Instantiate(monster_Mushroom, transform.position, Quaternion.identity);
        }
    }
}
