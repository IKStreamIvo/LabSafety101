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
        /* This is where input of Oculus Go 
         * I found:
         * VRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); 
         *  
         */

        Vector3 pos = Input.mousePosition;
        pos.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(pos);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            string name = hit.collider.name;
            if (name.Equals("Screen") || name.Equals("Start") || name.Equals("Options"))
            {
                transform.position = newPos;
                if(name.Equals("Start") || name.Equals("Options"))
                {
                    if (Input.GetMouseButtonDown(0))
                        menu.UseButton(name);
                }
            }
        }
    }
}
