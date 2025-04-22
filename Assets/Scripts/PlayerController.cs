using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    private float horizontalInput;
    private float verticalInput;
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * verticalInput * speed, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }

    }
}
