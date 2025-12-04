using UnityEngine;
using System.Collections;
// This script continuously spawns enemy prefabs from an array at a given interval that slowly shortens
// as the game goes on until the time cap is hit to prevent too many enemies from spawning at one time.

public class SpawnEnemies : MonoBehaviour
{
    public float waitForNextMonster = 1f;
    public bool spawnContinuously = true;
    public Game_Manager gameManager;
    public int spawnCounter = 0;
    public GameObject[] monsters;
    public Transform[] transforms;
    public bool isBossSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator SpawnEnemy()
    {
        while (spawnContinuously) {

            if(gameManager.currentGameState == Game_Manager.GameState.Playing)
            {
                yield return new WaitForSeconds(waitForNextMonster);
                int i = Random.Range(0, monsters.Length);
                int j = Random.Range(0, transforms.Length);
                Instantiate(monsters[i], transforms[j].transform.position, Quaternion.identity);

                if(!isBossSpawner)
                {

                    spawnCounter++;
                    if (spawnCounter >= 60)
                    {
                        waitForNextMonster = 2.0f;
                    }
                    else if (spawnCounter >= 40)
                    {
                        waitForNextMonster = 3.0f;
                    }
                    else if (spawnCounter >= 20)
                    {
                        waitForNextMonster = 3.5f;
                    }
                }
                
                   
            }
                
           
        }
        
    }
}
