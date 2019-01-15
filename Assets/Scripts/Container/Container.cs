using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private int _waistId;
    [SerializeField] private GameController.PickupType acceptsType;
    [SerializeField] private MeshRenderer highlightObject;
    [SerializeField] private Color targetedColor;
    private Color defaultColor;
    private bool targeted;
    private bool prevTargetState;

    public void PlaceObject(GameObject target)
    {
        InsideGlass liquid = target.GetComponentInChildren<InsideGlass>();
        ph phValue = liquid.phValue;
        if (phValue.CheckPHWaist(_waistId))
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect...");
        }
        Destroy(target);
    }

    public bool TargetHit(GameController.PickupType type)
    {
        if (acceptsType != type)
            return false;
        else
            return true;
    }

    public void Update()
    {/*
        if (highlightObject.enabled)
        {
            if (targeted)
            {
                Material mat = highlightObject.material;
                mat.color = targetedColor;
                highlightObject.material = mat;
            }
            else
            {
                Material mat = highlightObject.material;
                mat.color = defaultColor;
                highlightObject.material = mat;
            }
            prevTargetState = targeted;
            targeted = false; //to keep checking if we're being targeted
        }*/
    }
}
