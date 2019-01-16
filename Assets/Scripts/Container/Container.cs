using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private int _waistId;
    [SerializeField] private GameController.PickupType acceptsType;

    private bool targeted;
    private Color defaultColor;
    [SerializeField] private Color targetedColor;
    [SerializeField] private Renderer render;

    private bool prevTargetState;

    private void Start()
    {
        defaultColor = render.material.color;
    }

    public void Update()
    {
        if (targeted)
        {
            Material mat = render.material;
            mat.color = targetedColor;
            render.material = mat;
            Debug.Log("Targeted");
        }
        else
        {
            Material mat = render.material;
            mat.color = defaultColor;
            render.material = mat;
        }
        prevTargetState = targeted;
        targeted = false;
    }

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
            targeted = false;
        else
            targeted = true;

        return targeted;
    }
}
