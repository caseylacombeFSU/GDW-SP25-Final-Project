using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int lootCap = 20;
    private int enemyCap = 20;
    private int asteroidCap = 60;

    private int lootCount = 0;
    private int enemyCount = 0;
    private int asteroidCount = 0;

    public GameObject lootPrefab;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;

    private float spawnRangeX = 80.0f;
    private float spawnRangeY = 80.0f;

    private float startDelay = 0.0f;
    private float repeatRate = 2.0f;

    private int score = 0;
    private int co2Percent = 0;
    private int hullIntegrity = 100;

    private float timeIncrement = 1.0f;
    private float nextIncrement = 0.0f;
    private int co2Increment = 2;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI co2PercentText;
    public TextMeshProUGUI hullIntegrityText;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Main Scene")
        {
            InvokeRepeating("SpawnLoot", startDelay, repeatRate);
            InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Main Scene")
        {
            InvokeRepeating("SpawnAsteroid", startDelay, repeatRate);
            scoreText.text = "Money: " + score;
            co2PercentText.text = "CO2 Buildup: " + co2Percent + "%";
            hullIntegrityText.text = "Hull Integrity: " + hullIntegrity + "%";

            if (Time.time > nextIncrement)
            {
                nextIncrement = Time.time + timeIncrement;
                co2Percent += co2Increment;
            }

            if (hullIntegrity <= 0)
            {
                GameOver();
            }
            if (co2Percent >= 100)
            {
                GameOver();
            }
            if (lootCount >= lootCap)
            {
                GameOver();
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Title Scene")))
        {
            SceneManager.LoadScene(1);
        }
        

    }

    void SpawnLoot()
    {
        if (lootCount < lootCap)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
            Instantiate(lootPrefab, spawnPos, lootPrefab.transform.rotation);
            lootCount++;
        }
    }

    void SpawnEnemy()
    {
        if (enemyCount < enemyCap)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
            enemyCount++;
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidCount < asteroidCap)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, asteroidPrefab.transform.rotation);
            asteroid.transform.localScale = Vector3.one * Random.Range(1.0f, 8.0f);
            asteroidCount++;
        }
    }

    public void IncreaseScore(int money)
    {
        score += money;
    }

    public void DecreaseCO2Percent(int percent)
    {
        co2Percent -= percent;
    }

    public void DecreaseHullIntegrity(int percent)
    {
        hullIntegrity -= percent;
    }

    public void increaseHullIntegrity(int percent)
    {
        hullIntegrity += percent;
    }

    public void GameOver()
    {
        
    }

}
