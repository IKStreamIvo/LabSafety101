using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskBehaviour : MonoBehaviour
{
    public int _id;
    [SerializeField] private TextMeshProUGUI textbox;

    [SerializeField] private Image m_Image;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite check;
    [SerializeField] private Sprite messedUp;
    private string tasky = "";

    void Start()
    {
        m_Image.sprite = normal;
    }

    public void SetTask(int index, string task)
    {
        _id = index;
        tasky = task;
        textbox.SetText(task);
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

    void Update() {
        textbox.SetText(tasky);
    }
}
