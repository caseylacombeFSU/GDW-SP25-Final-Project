using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    private int lootCap = 30;
    private int enemyCap = 20;
    private int asteroidCap = 80;
    private int o2TankCap = 15;

    private int lootCount = 0;
    private int enemyCount = 0;
    private int asteroidCount = 0;
    private int o2TankCount = 0;

    public GameObject lootPrefab;
    public GameObject enemyPrefab;
    public GameObject asteroidPrefab;
    public GameObject o2TankPrefab;

    private float spawnRangeX = 120.0f;
    private float spawnRangeY = 120.0f;

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

    public GameObject pauseScreen;
    public bool isGamePaused = false;

    public GameObject loseScreen;
    public GameObject hullLoseText;
    public GameObject CO2LoseText;
    public GameObject winScreen;

    public Slider co2PercentSlider;
    public Slider hullIntegritySlider;

    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if (sceneName == "Main Scene")
        {
            InvokeRepeating("SpawnLoot", startDelay, repeatRate);
            InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
            InvokeRepeating("SpawnO2Tank", startDelay, repeatRate);
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
            //co2PercentText.text = "CO2 Buildup: " + co2Percent + "%";
            co2PercentSlider.value = co2Percent;
            //hullIntegrityText.text = "Hull Integrity: " + hullIntegrity + "%";
            hullIntegritySlider.value = hullIntegrity;

            if (Time.time > nextIncrement)
            {
                nextIncrement = Time.time + timeIncrement;
                co2Percent += co2Increment;
            }

            if (hullIntegrity <= 0)
            {
                GameOverLoss();
            }
            if (co2Percent >= 100)
            {
                GameOverLoss();
            }
            if (score >= 100)
            {
                GameOverWin();
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Title Scene")))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }

        Pause();
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

    void SpawnO2Tank()
    {
        if (o2TankCount < o2TankCap)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
            Instantiate(o2TankPrefab, spawnPos, o2TankPrefab.transform.rotation);
            o2TankCount++;
        }
    }

    public void IncreaseScore(int money)
    {
        score += money;
    }

    public void DecreaseCO2Percent(int percent)
    {
        if (co2Percent - percent < 0)
        {
            co2Percent = 0;
        }
        else
        {
            co2Percent -= percent;
        }
    }

    public void DecreaseHullIntegrity(int percent)
    {
        hullIntegrity -= percent;
    }

    public void increaseHullIntegrity(int percent)
    {
        if (hullIntegrity - percent > 100)
        {
            hullIntegrity = 100;
        }
        else
        {
            hullIntegrity += percent;
        }
    }

    public void DecreaseLootCount()
    {
        lootCount--;
    }

    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }

    public void DecreaseO2TankCount()
    {
        o2TankCount--;
    }

    public void Pause()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    public void GameOverLoss()
    {
        loseScreen.SetActive(true);
        if(hullIntegrity <= 0)
        {
            CO2LoseText.SetActive(false);
        }
        if (co2Percent >= 100)
        {
            hullLoseText.SetActive(false);
        }
        Time.timeScale = 0;
    }

    public void GameOverWin()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
