using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private float cameraXBound = 104.0f;
    private float cameraYBound = 120.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > cameraXBound)
        {
            transform.position = new Vector3(cameraXBound, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.y > cameraYBound)
        {
            transform.position = new Vector3(transform.position.x, cameraYBound, transform.position.z);
        }
        else if (player.transform.position.x < -cameraXBound)
        {
            transform.position = new Vector3(-cameraXBound, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.y < -cameraYBound)
        {
            transform.position = new Vector3(transform.position.x, -cameraYBound, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        
    }
}
