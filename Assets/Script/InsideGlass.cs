using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideGlass : MonoBehaviour {
    
    public float maxSize = 0.4f;
    private float _currentsize = 0f;

    public void SizeLiquid(float max, float current)
    {
        float newsize = current/max*100;
        _currentsize = maxSize / 100 * newsize;

        if (_currentsize > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, _currentsize, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, _currentsize + 0.02f, transform.localPosition.z);
        }
    }

}
