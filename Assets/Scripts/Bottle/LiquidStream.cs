﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Project

public class LiquidStream : MonoBehaviour
{
    // Content of stream
    public GameObject beaker;
    private ParticleSystem ps;
    public float emissionValue = 5.0f;
    private float _stream = 150f;
    private float _liquid = 0;

    // Timer
    private float _countDown = 0f;

    // Color of stream
    private Renderer rend;
    private ph _phValue;

    void Start()
    {
        // get the particle system and renderer of stream
        ps = GetComponent<ParticleSystem>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Change emission
        var emission = ps.emission;
        emission.rateOverTime = emissionValue;
    }

    // Change size of stream
    public void ChangeValue(float value, float liquid)
    {
        emissionValue = _stream * value;
        _liquid = liquid;
    }

    public void ChangeColor(Color clr)
    {
        rend.material.color = clr;
        var main = ps.main;
        main.startColor = clr;
    }

    // Change color of stream
    public void ChangePH(ph phValue)
    {
        _phValue = phValue;
    }

    // When stream hits something
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other);
        Rigidbody otherRb = other.GetComponent<Rigidbody>();

        // If the other is not this bottle
        if(beaker.GetComponent<Rigidbody>() != otherRb) {
            // Close stream
            var collision = ps.collision;
            collision.lifetimeLoss = .8f;

            // Get codes of the bottles
            Bottle parentBottle = beaker.GetComponent<Bottle>();
            Bottle otherBottle = other.GetComponent<Bottle>();

            // If the bottles are found add liquid to other bottle
            if (parentBottle != null && otherBottle != null)
            {
                _countDown -= Time.deltaTime;
                if (_countDown <= 0)
                {
                    otherBottle.GainLiquid(_liquid, _phValue);
                    _countDown = 0.1f;
                }
            }
        }
        else
        {
            // Open stream
            var collision = ps.collision;
            //collision.lifetimeLoss = 0;
        }
    }
}