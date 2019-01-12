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
}

public class phValues : MonoBehaviour
{
    List<ph> valueList = new List<ph>();

    // Start is called before the first frame update
    void Start()
    {
        valueList.Add(new ph(0, new Color(229, 32, 39, 0)));
        valueList.Add(new ph(1, new Color(234, 85, 46, 0)));
        valueList.Add(new ph(2, new Color(246, 160, 40, 0)));
        valueList.Add(new ph(3, new Color(255, 208, 26, 0)));
        valueList.Add(new ph(4, new Color(220, 221, 48, 0)));
        valueList.Add(new ph(5, new Color(151, 194, 44, 0)));
        valueList.Add(new ph(6, new Color(84, 175, 50, 0)));
        valueList.Add(new ph(7, new Color(11, 155, 57, 0)));
        valueList.Add(new ph(8, new Color(0, 163, 89, 0)));
        valueList.Add(new ph(9, new Color(46, 181, 178, 0)));
        valueList.Add(new ph(10, new Color(5, 134, 201, 0)));
        valueList.Add(new ph(11, new Color(39, 82, 160, 0)));
        valueList.Add(new ph(12, new Color(57, 62, 144, 0)));
        valueList.Add(new ph(13, new Color(68, 57, 142, 0)));
        valueList.Add(new ph(14, new Color(57, 41, 126, 0)));
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
