using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 5.0f;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 mousePosition = Input.mousePosition + new Vector3(0, 0, 10);
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        
        if(direction != Vector2.zero)
        {
            transform.forward = direction;
        }
        */
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        //moveVelocity = moveInput * sensitivity;

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane backPlane = new Plane(Vector3.forward, Vector3.zero);
        float rayLength;

        if (backPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

            transform.LookAt(new Vector3(pointToLook.x, pointToLook.y, transform.position.z));
        }



        //turn.x += Input.GetAxis("Mouse X") * sensitivity;

        //turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        //transform.localRotation = Quaternion.Euler(0, 0, -turn.x);

        /*
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));

       
        //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //worldPoint.z = 0;

        Vector3 difference = point - transform.position;
        difference.Normalize();

        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
        */

    }
}
