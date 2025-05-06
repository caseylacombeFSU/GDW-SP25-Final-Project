using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    private GameManager gameManager;

    private Rigidbody playerRB;

    private float horizontalInput;
    private float verticalInput;
    private float speed = 15.0f;

    private float playerXBound = 75.0f;
    private float playerYBound = 100.0f;

    private float fireRate = 0.1f;
    private float nextShot = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > playerXBound)
        {
            transform.position = new Vector3(playerXBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.y > playerYBound)
        {
            transform.position = new Vector3(transform.position.x, playerYBound, transform.position.z);
        }
        else if (transform.position.x < -playerXBound) 
        {
            transform.position = new Vector3(-playerXBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -playerYBound)
        {
            transform.position = new Vector3(transform.position.x, -playerYBound, transform.position.z);
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
            playerRB.velocity = movement * speed;
        }

        
        

        //transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed, Space.World);
        //transform.Translate(Vector3.up * Time.deltaTime * verticalInput * speed, Space.World);

        if (Input.GetMouseButton(0))
        {
            if (Time.time > nextShot)
            {
                nextShot = Time.time + fireRate;
                Instantiate(projectile, transform.TransformPoint(Vector3.forward * 1.1f), transform.rotation);
            }
            
        }

        if (transform.position.z != 0.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        }

    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            gameManager.IncreaseScore(5);
            gameManager.DecreaseLootCount();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            gameManager.DecreaseHullIntegrity(10);
        }
        else if (collision.gameObject.CompareTag("O2Tank"))
        {
            gameManager.DecreaseO2TankCount();
            gameManager.DecreaseCO2Percent(5);
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            gameManager.DecreaseHullIntegrity(10);
            gameManager.DecreaseEnemyCount();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            gameManager.DecreaseHullIntegrity(5);
            Destroy(other.gameObject);
        }
    }

}
