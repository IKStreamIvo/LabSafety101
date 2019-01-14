using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilerEffect : MonoBehaviour
{
    [SerializeField] private bool boiling = false;
    private ParticleSystem boilerEffect;

    private float _countDown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        boilerEffect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = boilerEffect.emission;
        if (boiling)
            emission.rateOverTime = 10f;
        else
            emission.rateOverTime = 0f;

        _countDown -= Time.deltaTime;
        if (_countDown <= 0)
        {
            boiling = false;
            _countDown = 2f;
        }
    }

    public void StartBoiling()
    {
        boiling = true;
    }

    public void ChangeColor(Color clr)
    {
        var settings = boilerEffect.main;
        settings.startColor = clr;

        Color color = new Color(clr.r, clr.g, clr.b, 0.5f);
        Renderer rend = boilerEffect.GetComponent<Renderer>();
        rend.material.SetColor("_Color", color);
    }
}
