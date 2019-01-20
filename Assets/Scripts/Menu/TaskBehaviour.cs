using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBehaviour : MonoBehaviour
{
    public int _id;
    [SerializeField] private Text textbox;

    [SerializeField] private Image m_Image;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite check;
    [SerializeField] private Sprite messedUp;

    void Start()
    {
        m_Image.sprite = normal;
    }

    public void SetTask(int index, string task)
    {
        _id = index;
        textbox.text = task;
    }
    public void ResetTask()
    {
        m_Image.sprite = normal;
    }
    public void Check()
    {
        m_Image.sprite = check;
    }
    public void MessedUp()
    {
        m_Image.sprite = messedUp;
    }

}
