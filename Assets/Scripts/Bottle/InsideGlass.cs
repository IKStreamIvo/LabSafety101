using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Color start;
    public ph phValue;
    private ph _lastAddedPh;
    private float _speed = 2f;
    private float _timer = 0f;
    public LiquidStream liquidStream;
    private phValues phList = new phValues();
    public TextMeshPro phDisplay;

    [SerializeField] private BoilerEffect boiling;

    void Awake()
    {
        // Set start ph value
        phValue = phList.RandomPHValue();
        _rendLiquid = GetComponent<Renderer>();
        start = phValue.GetColor();
        _rendLiquid.material.color = start;

        phDisplay.SetText(phValue.GetId().ToString());
    }

    void Update()
    {
        // When changing color
        if (changeColor)
        {
            // Slowly go to other color
            Color newColor = Color.Lerp(start, phValue.GetColor(), _timer);
            _rendLiquid.material.color = newColor;
            _rendLiquid.material.SetColor("_EmissionColor", newColor);

            liquidStream.ChangeColor(newColor);
            boiling.ChangeColor(newColor);

            // Current color is start color
            start = newColor;

            // Go to next color
            if (_timer < 1f)
                _timer += Time.deltaTime / _speed;

            // When the color has changed to end color
            if (_timer >= 1f)
            {
                liquidStream.ChangePH(phValue);
                start = phValue.GetColor();
                _timer = 0f;
                changeColor = false;
            }
        }

        if(GetComponentInParent<Bottle>().content == 0f) {
            phDisplay.enabled = false;
        } else {
            phDisplay.enabled = true;
        }
    }

    // Change the ph value
    public void ChangePH(ph addedphValue)
    {
        if (_lastAddedPh != addedphValue)
        {
            phValue = phList.MixingValues(phValue, addedphValue);
            changeColor = true;
            _lastAddedPh = addedphValue;
            phDisplay.SetText(phValue.GetId().ToString());
        }
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

    public void ForcePH(ph newPh) {
        phValue = newPh;

        start = phValue.GetColor();
        _rendLiquid.material.color = start;
        _rendLiquid.material.SetColor("_EmissionColor", start);

        phDisplay.SetText(phValue.GetId().ToString());
    }
}
