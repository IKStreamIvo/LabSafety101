using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Project

public class InsideGlass : MonoBehaviour
{
    // What is needed to change the size of the liquid
    public float maxSize = 0.4f;
    private float _currentsize = 0f;

    // What is needed to change the color of the liquid
    public bool changeColor = false;
    private Renderer _rendLiquid;
    private Renderer _rendStream;
    public Color start, end;
    private float _speed = 2f;
    private float _timer = 0f;
    public LiquidStream liquidStream;

    void Start()
    {
        // Set start color
        _rendLiquid = GetComponent<Renderer>();
        _rendLiquid.material.color = start;
    }

    void Update()
    {
        // When changing color
        if (changeColor)
        {
            // Slowly go to other color
            Color newColor = Color.Lerp(start, end, _timer);
            _rendLiquid.material.color = newColor;
            liquidStream.ChangeColor(newColor);

            // Current color is start color
            start = newColor;

            // Go to next color
            if (_timer < 1f)
                _timer += Time.deltaTime / _speed;

            // When the color has changed to end color
            if (_timer >= 1f)
            {
                start = end;
                _timer = 0f;
                changeColor = false;
            }
        }
    }

    // Start to change the color to clr
    public void ChangeColor(Color clr)
    {
        end = clr;
        changeColor = true;
    }

    // Change to new size
    public void SizeLiquid(float max, float current)
    {
        // Calculate new size
        float newsize = current / max * 100;
        _currentsize = maxSize / 100 * newsize;

        if (_currentsize > 0)
        {
            // Change size
            transform.localScale = new Vector3(transform.localScale.x, _currentsize, transform.localScale.z);
            // Reposition the liquid to bottom of the bottle
            transform.localPosition = new Vector3(transform.localPosition.x, _currentsize + 0.02f, transform.localPosition.z);
        }
    }

}
