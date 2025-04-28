using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public GameObject player;
    public GameObject projectile;
    public GameManager gameManager;

    //public Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        //playerRB = player.gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            gameManager.IncreaseScore(5);
            gameManager.DecreaseCO2Percent(3);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
