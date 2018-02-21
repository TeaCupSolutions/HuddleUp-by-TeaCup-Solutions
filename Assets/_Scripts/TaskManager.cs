using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour {

	public List<Task> tasks = new List<Task> ();
	public List<Task> activeTasks = new List<Task> ();
	public TMP_Text title, subtasks;

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
			
			activeTasks.RemoveAt (remove);
			activeTasks.TrimExcess (); //might not work....

			//add new task to the list

			//for each task in our task list
			foreach (Task nextTask in tasks) {
				
				//if task is not completed yet
				if (!nextTask.IsCompleted) {

					//add that task to the active task list
					activeTasks.Add(nextTask);
					break;
				}
			}

		}

		title.text = activeTasks[0].description;
		subtasks.text = activeTasks[0].subtasksToString();
		
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
