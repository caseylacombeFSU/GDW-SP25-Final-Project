using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public GameObject player;
    public GameObject projectile;
    public GameManager gameManager;

    private Rigidbody enemyRB;
    private GameObject player;

    private int health = 10;
    private int speed = 2;

    private int playerBound = 10;
    private int nearPlayer = 20;

    private float fireRate = 1.0f;
    private float nextShot = 0.0f;

    //public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        //playerRB = player.gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //using nested ifs here even though they could be in one line for readablility
        //checks if the enemy is near enough to a player to "notice" them, then checks if they are not too close to a player
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < nearPlayer || Mathf.Abs(transform.position.y - player.transform.position.y) < nearPlayer)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) > playerBound || Mathf.Abs(transform.position.y - player.transform.position.y) > playerBound)
            {
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                enemyRB.AddForce(lookDirection * speed);
                
                if (Time.time > nextShot)
                {
                    transform.LookAt(player.transform);
                    nextShot = Time.time + fireRate;
                    Instantiate(projectile, transform.position, transform.rotation);
                }
            }
        }

        
        
    }

    IEnumerator EnemyProjectileCountownRoutine()
    {
        yield return new WaitForSeconds(1);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            health--;
            if(health <= 0)
            {
                DestroyEnemy();
            }
        }
    }

    public void DestroyEnemy()
    {
        gameManager.IncreaseScore(5);
        gameManager.DecreaseCO2Percent(3);
        Destroy(gameObject);
    }
}
