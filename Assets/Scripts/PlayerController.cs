using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    private GameManager gameManager;

    private Rigidbody playerRB;

    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        playerRB.velocity = movement * speed;
        

        //transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed, Space.World);
        //transform.Translate(Vector3.up * Time.deltaTime * verticalInput * speed, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.TransformPoint(Vector3.forward * 1.1f), transform.rotation);
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
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            gameManager.DecreaseHullIntegrity(10);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.DecreaseHullIntegrity(10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy Projectile"))
        {
            gameManager.DecreaseHullIntegrity(5);
            Destroy(collision.gameObject);
        }
    }

}
