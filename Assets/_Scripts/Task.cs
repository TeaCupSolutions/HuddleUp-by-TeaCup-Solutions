using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public string description;
    public bool isCompleted = false;
    bool isActive;
    public List<SubTask> subtasks;
    public List<SubTask> challengingSubtasks;

    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    void AddSubtask(SubTask newSubTask)
    {
        subtasks.Add(newSubTask);
    }

    // Update is called once per frame
    void Update()
    {

        //check if task is completed
        if (!this.IsCompleted)
        {
            bool taskStatus = true;
            foreach (SubTask subTask in subtasks)
            {
                if (!subTask.IsCompleted)
                {
                    taskStatus = false;
                }
            }
            if (taskStatus)
            {
                Debug.Log(description + " Task Completed");
            }
            this.IsCompleted = taskStatus;
        }
    }


    public string subtasksToString()
    {
        string output = "";
        int count = 1;
        foreach (SubTask s in subtasks)
        {
            if (s.IsCompleted)
            {
                output += "<s>" + count + ". <indent=10%>" + s.description + "</indent></s>\n";
            }
            else
            {
                output += count + ". <indent=10%>" + s.description + "</indent>\n";
            }
            count++;
        }
        return output;
    }
}
