using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

     [SerializeField] private GameObject[] powerUp;
     [SerializeField] private GameObject[] enemy;
     [SerializeField] private Vector2[] enemySpawnPoints;

     void Start()
     {
          StartCoroutine(SpawnPowerUp());
          StartCoroutine(SpawnEnemy());
     }

     IEnumerator SpawnPowerUp()
     {
          while (true)
          {
               yield return new WaitForSeconds(Random.Range(5, 15));
               float x = Random.Range(-18f, 10f);
               float y = Random.Range(-18f, 10f);
               Instantiate(powerUp[0], new Vector2(x, y), Quaternion.identity);
          }
     }

     IEnumerator SpawnEnemy()
     {
          while (true)
          {
               yield return new WaitForSeconds(Random.Range(1, 4));
               int index = Random.Range(0, enemySpawnPoints.Length);
               Vector2 spawnPos = enemySpawnPoints[index];
               Debug.Log("Spawning enemy at: " + spawnPos); 
               Instantiate(enemy[0], spawnPos, Quaternion.identity);
          }
     }



}