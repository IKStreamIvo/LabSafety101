using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Project

public class Bottle : MonoBehaviour
{
    // The content of bottle
    public float content = 1000f;
    public float maxContent = 2000f;
    private float stream = 0;
    public float maxStream = 25f;
    private float liquid = 0f;

    // What you need to change content or stream
    private float _countDown = 0f;
    public float speed = 10f;

    // Stream and liquid
    public LiquidStream currentParticles;
    public InsideGlass sizeLiquid;

    void Update()
    {
        // How wide is stream at the current angle?
        stream = CheckValue(transform.rotation.eulerAngles.z);

        // How wide is stream with the content?
        _countDown -= Time.deltaTime;
        if(_countDown <= 0)
        {
            liquid = maxStream * (stream * 2);

            if (content <= 0)
                liquid = 0;
            else if (content < liquid)
                liquid = content;

            content -= liquid;

            _countDown = 0.1f;
        }

        // Stream is empty if there is no content
        if (liquid == 0)
            stream = 0;

        // Change stream
        currentParticles.ChangeValue(stream, liquid);
        // Change content
        sizeLiquid.SizeLiquid(maxContent, content);
    }

    // Change angle to stream
    private float CheckValue(float angle)
    {
        if(angle <= 0)
        {
            angle = angle * -1;
        }

        float percentage = (angle / 360) * 100;
        if (percentage > 50)
            percentage = 100 - percentage;

        float stream;
        if (percentage > 15)
            stream = percentage / 100;
        else
            stream = 0;

        return stream;
    }

    // Add content to bottle
    public void GainLiquid(float liquid)
    {
        if(content < maxContent)
            content += liquid;
    }
}
