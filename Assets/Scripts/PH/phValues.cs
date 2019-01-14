using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ph
{
    private int _id;
    private Color _clr;

    public ph(int number, Color color)
    {
        _id = number;
        _clr = color;
    }

    public int GetId() { return _id; }
    public Color GetColor() { return _clr; }


    override public string ToString()
    {
        return _id + " Color: " + _clr;
    }
}

public class phValues
{
    List<ph> valueList = new List<ph>();

    public phValues()
    {
        valueList.Add(new ph(0,  new Color(0.229f, 0.032f, 0.039f, 0f)));
        valueList.Add(new ph(1,  new Color(0.234f, 0.085f, 0.046f, 0f)));
        valueList.Add(new ph(2,  new Color(0.246f, 0.160f, 0.040f, 0f)));
        valueList.Add(new ph(3,  new Color(0.255f, 0.208f, 0.026f, 0f)));
        valueList.Add(new ph(4,  new Color(0.220f, 0.221f, 0.048f, 0f)));
        valueList.Add(new ph(5,  new Color(0.151f, 0.194f, 0.044f, 0f)));
        valueList.Add(new ph(6,  new Color(0.084f, 0.175f, 0.050f, 0f)));
        valueList.Add(new ph(7,  new Color(0.011f, 0.155f, 0.057f, 0f)));
        valueList.Add(new ph(8,  new Color(    0f, 0.163f, 0.089f, 0f)));
        valueList.Add(new ph(9,  new Color(0.046f, 0.181f, 0.178f, 0f)));
        valueList.Add(new ph(10, new Color(0.005f, 0.134f, 0.201f, 0f)));
        valueList.Add(new ph(11, new Color(0.039f, 0.082f, 0.160f, 0f)));
        valueList.Add(new ph(12, new Color(0.057f, 0.062f, 0.144f, 0f)));
        valueList.Add(new ph(13, new Color(0.068f, 0.057f, 0.142f, 0f)));
        valueList.Add(new ph(14, new Color(0.057f, 0.041f, 0.126f, 0f)));
    }

    public ph RandomPHValue()
    {
        int index = Random.Range(0, valueList.Count);
        return valueList[index];
    }

    public ph MixingValues(ph phOne, ph phTwo)
    {
        int one = phOne.GetId();
        int two = phTwo.GetId();

        int index = Mathf.RoundToInt((one + two) / 2);

        return valueList[index];
    }
}
