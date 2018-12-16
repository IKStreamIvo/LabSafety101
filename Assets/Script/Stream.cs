using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public GameObject beaker;
    private ParticleSystem ps;
    public float emissionValue = 5.0f;
    private float _stream = 150f;
    private float _liquid = 0;

    private float _countDown = 0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var emission = ps.emission;
        emission.rateOverTime = emissionValue;
    }

    public void ChangeValue(float value, float liquid)
    {
        emissionValue = _stream * value;
        _liquid = liquid;
    }

    void OnParticleCollision(GameObject other)
    {
        if(!beaker.name.Equals(other.transform.parent.name))
        {
            Bottle parentBottle = beaker.GetComponent<Bottle>();

            GameObject otherParent = other.transform.parent.gameObject;
            Bottle otherBottle = otherParent.GetComponent<Bottle>();

            if (parentBottle != null && otherBottle != null)
            {
                _countDown -= Time.deltaTime;
                if (_countDown <= 0)
                {
                    otherBottle.GainLiquid(_liquid);
                    _countDown = 0.1f;
                }
            }
        }
    }
}
