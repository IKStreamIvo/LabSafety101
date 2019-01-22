using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCurser : MonoBehaviour
{
    [SerializeField] private Transform screen;
    [SerializeField] private MenuButtons menu;

    void Update()
    {
        // For Oculus Go
        // Vector2 joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        // Vector3 movement = new Vector3(joystick.x, joystick.y, 0);

        // For PC
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        movement = movement * 0.01f;

        Vector3 euler = screen.eulerAngles;
        screen.eulerAngles = new Vector3(0, 0, 0);

        transform.Translate(movement);

        float height = screen.localScale.y;
        float width = screen.localScale.x;

        if (transform.position.z > (screen.position.z + (0.45f*width)))
            transform.position = new Vector3(transform.position.x, transform.position.y, screen.position.z + (width*0.45f));
        else if (transform.position.z < (screen.position.z - (0.446f*width)))
            transform.position = new Vector3(transform.position.x, transform.position.y, screen.position.z - (width*0.446f));

        if (transform.position.y > (screen.position.y + (height*0.626f)))
            transform.position = new Vector3(transform.position.x, screen.position.y + (height*0.626f), transform.position.z);
        if (transform.position.y < (screen.position.y - (height*0.6f)))
            transform.position = new Vector3(transform.position.x, screen.position.y - (height*0.6f), transform.position.z);

        screen.eulerAngles = euler;

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            string name = hit.collider.name;
            if (name.Equals("Start") || name.Equals("Options"))
            {
                // for pc
                if (Input.GetKeyDown("space"))
                    menu.UseButton(name);

                // for Oculus Go
                // if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
                    // menu.UseButton(name);
            }

        }
    }
}
