using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom : MonoBehaviour
{
    private float t = 0.0f;
    [SerializeField] private List<GameObject> darkWalls = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject wall in darkWalls)
        {
            Renderer rend = wall.GetComponent<Renderer>();
            rend.material.SetFloat("_Metallic", Mathf.Lerp(0, 1, t));
            t += 0.5f * Time.deltaTime;
        }
    }
}
