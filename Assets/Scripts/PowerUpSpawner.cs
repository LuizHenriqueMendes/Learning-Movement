using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

     [SerializeField] private GameObject[] powerUp;

     void Start()
     {
          StartCoroutine(SpawnPowerUp());
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


}