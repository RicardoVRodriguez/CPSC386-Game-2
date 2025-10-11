using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour
{
    public float waitForNextMonster = 1f;
    public bool spawnContinuously = true;
    public Game_Manager gameManager;
    
    public GameObject[] monsters;
    public Transform[] transforms;
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
                int i = Random.Range(0, monsters.Length);
                int j = Random.Range(0, transforms.Length);
                Instantiate(monsters[i], transforms[j].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(waitForNextMonster);
            }
                
           
        }
        
    }
}
