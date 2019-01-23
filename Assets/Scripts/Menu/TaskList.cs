using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    private List<string> tasks = new List<string>();

    [SerializeField] private GameObject prefabTask;
    [SerializeField] private RectTransform parentTaskList;
    private List<TaskBehaviour> behaviours = new List<TaskBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        tasks.Add("Pick up a beaker with a chemical in it and place it on the left side of the grid");
        tasks.Add("Pick up a second beaker with a chemical in it and place it on the right side of the grid");
        tasks.Add("Pick up an empty beaker from the table on the right and place it in between the other two beakers on the grid");
        tasks.Add("Pour both chemicals into the empty beaker");
        tasks.Add("Dispose of the chemical into the correct waste container on the left table.");
        
        //CreateTaskList();
    }

    private void CreateTaskList()
    {
        float ySize = parentTaskList.rect.height / (tasks.Count + 1) / 5;
        float xPosition = parentTaskList.rect.width / 100 * 2;
        behaviours = new List<TaskBehaviour>();
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject task = Instantiate(prefabTask, parentTaskList);
            task.transform.position += new Vector3(xPosition, (-ySize * (i + 1)), 0f);
            Vector3 pos = task.transform.localPosition;
            pos.z = 0f;
            task.transform.localPosition = pos;
            task.GetComponent<TaskBehaviour>().SetTask(i, tasks[i]);
            behaviours.Add(task.GetComponent<TaskBehaviour>());
        }
    }

    private void CheckTask(int index)       {   behaviours[index].Check();     }
    private void MessedUpTask(int index)    {   behaviours[index].MessedUp();  }

    public void ResetLevel()
    {
        DebugText.VRLog("Reset Level");
        foreach(TaskBehaviour behaviour in behaviours)
        {
            behaviour.ResetTask();
        }
        CreateTaskList();
    }
}
