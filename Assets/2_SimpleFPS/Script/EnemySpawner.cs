using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate;

    private float timer = 0f;
    private const float SpawnRange = 50f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > spawnRate)
        {
            timer -= spawnRate;
            
            var posX = Random.Range(-SpawnRange, SpawnRange);       
            var posY = Random.Range(-SpawnRange, SpawnRange);       
            
            var tempEnemy = Instantiate(enemy,new Vector3(posX,0f,posY), Quaternion.identity).GetComponent<Enemy>();       // rotation은 기본
            tempEnemy.Init(Random.Range(70f,130f));
        }
    }
}
