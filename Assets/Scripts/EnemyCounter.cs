using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    public static EnemyCounter instance;
    public static bool canSpawn;
    [SerializeField] private float maxCount;
    public TextMeshProUGUI scoreText;
    private float enemyCount = 0;

    void Awake()
    {        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        canSpawn = maxCount > enemyCount;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Enemies Remaining: " + enemyCount;
    }

    public void AddEnemy()
    {
        enemyCount += 1;
        UpdateScoreText();
    }

    public void RemoveEnemy()
    {
        enemyCount -= 1;
        UpdateScoreText();
    }
}
