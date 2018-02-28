using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour {

	public List<Task> tasks = new List<Task> ();
	public List<Task> activeTasks = new List<Task> ();
    public TMP_Text title, subtasks, title1, subtasks2;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		int remove = -1;

		checkWin(); // returns -1 if gameOver

        if (checkWin() == -1) {
            Debug.Log("All tasks complete");
        }

		//loop through each task, stop if we hit completed task
		for(int i =0; i < activeTasks.Capacity; i++) {

			if (activeTasks[i].IsCompleted) {
				remove = i;
				break;
			}

		}

		//if we found something completed, remove it from the list
		if (remove != -1) {
			foreach (Task nextTask in tasks) {
				
				//if task is not completed yet
				if (!nextTask.IsCompleted) {

					//add that task to the active task list
					activeTasks[remove] = nextTask;
					break;
				}
			}

		}

        if (activeTasks[0])
        {
            title.text = activeTasks[0].description;
            subtasks.text = activeTasks[0].subtasksToString();
        } else if (activeTasks[1])
        {
            title1.text = activeTasks[1].description;
            subtasks2.text = activeTasks[1].subtasksToString();
        }

    }

	int checkWin(){

		//if all tasks are completed, game win.a

		foreach (Task task in tasks) {
			if (!task.IsCompleted) {
				return 0;
			}
		}

		return -1;
	}
		




}
