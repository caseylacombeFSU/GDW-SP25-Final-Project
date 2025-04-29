using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float bound = 125.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > bound || transform.position.x < -bound || transform.position.y > bound || transform.position.y < -bound)
        {
            Destroy(gameObject);
        }
    }
}
