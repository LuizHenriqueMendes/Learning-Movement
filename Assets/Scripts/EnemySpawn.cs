using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;    
    private float spawnTime;    

    void Awake()
    {
        SetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0 && EnemyCounter.canSpawn)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            EnemyCounter.instance.AddEnemy();
            SetSpawnTimer();
        }

    }

    private void SetSpawnTimer()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}
