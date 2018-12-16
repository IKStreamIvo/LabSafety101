using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public float content = 1000f;
    public float maxContent = 2000f;
    private float stream = 0;
    public float maxStream = 25f;
    private float liquid = 0f;
    private float _countDown = 0f;

    public float speed = 10f;

    public bool move = false;
    private bool moveLeft = false;

    public bool rotate = false;

    public Stream currentParticles;
    public InsideGlass sizeLiquid;

    void Update()
    {
        if (move)
        {
            float y = 0;
            if (moveLeft)
                y = 1;
            else
                y = -1;

            transform.Translate(new Vector3(0, y, 0) * Time.deltaTime * speed);

            if (transform.position.x < -10 || transform.position.x > 10)
                moveLeft = !moveLeft;
        }

        if (rotate)
        {
            transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
        }

        stream = CheckValue(transform.rotation.eulerAngles.z);

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

        if (liquid == 0)
            stream = 0;

        currentParticles.ChangeValue(stream, liquid);
        sizeLiquid.SizeLiquid(maxContent, content);
    }

    private void OnMouseDown()
    {
        rotate = !rotate;
    }

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

    public void GainLiquid(float liquid)
    {
        if(content < maxContent)
            content += liquid;
    }
}
