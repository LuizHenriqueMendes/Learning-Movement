using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{

    [SerializeField] GameObject landMine;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            Destroy(landMine);
        }
    }
} 
