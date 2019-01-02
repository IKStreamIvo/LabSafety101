using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidInBottle : MonoBehaviour {
    public GameObject mLiquid;
    public GameObject mLiquidMesh;

    private int _sloshSpeed = 10;
    private int _rotationSpeed = 15;

    private int _difference = 5;

	void Update ()
    {
        // Motion
        Slosh();

        // Rotation
        float x = Time.deltaTime * _rotationSpeed;
        mLiquidMesh.transform.rotation = Quaternion.Euler(x, 0, 0);
        //mLiquidMesh.transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.Self);
	}

    private void Slosh()
    {
        // Inverse cup rotation
        Quaternion inverseRotation = Quaternion.Inverse(transform.localRotation);
        Debug.Log("Inverse: "+ inverseRotation);

        // Rotate to
        Vector3 finalRotation = Quaternion.RotateTowards(mLiquid.transform.localRotation, inverseRotation, _sloshSpeed * Time.deltaTime).eulerAngles;
        Debug.Log("Finale: " + finalRotation);

        // Clamp
        finalRotation.x = ClampRotationValue(finalRotation.x, _difference);
        finalRotation.y = ClampRotationValue(finalRotation.y, _difference);
        finalRotation.z = ClampRotationValue(finalRotation.z, _difference);

        Debug.Log("Finale 2: " + finalRotation);

        // Set
        mLiquid.transform.localEulerAngles = finalRotation;
    }

    private float ClampRotationValue(float value, float difference)
    {
        float returnValue = 0.0f;

        if(value > 180)
        {
            // Clamp
            returnValue = Mathf.Clamp(value, 360 - difference, 360);
        }
        else
        {
            // Clamp
            returnValue = Mathf.Clamp(value, 0, difference);
        }

        return returnValue;
    }
}
