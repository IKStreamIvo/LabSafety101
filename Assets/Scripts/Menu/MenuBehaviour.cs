﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    public bool followCamera = true;
    [SerializeField] private Transform _camera;
    private Vector3 offset;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject taskMenu;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - _camera.position;
        /*if(followCamera) {
            transform.SetParent(_camera, true);
        }*/
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followCamera)
        {
            mainMenu.SetActive(true);
            taskMenu.SetActive(false);
            transform.position = new Vector3(_camera.position.x, _camera.position.y, _camera.position.z);
            transform.rotation = _camera.transform.rotation * Quaternion.Euler(0, 90f, 0);
            //transform.LookAt(GameController.mainCamera);
        }
        else
        {
            mainMenu.SetActive(false);
            taskMenu.SetActive(true);
            transform.position = new Vector3(-3.515f, 0.567f, -1.2f);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
