using System;
using System.Collections.Generic;
using UnityEngine;

public enum SubTaskAction
{
    ReplaceInScene,
    Destroy,
    MakeInteractableVisible,
    MakeInteractableInvisible,
    Nothing,
}

public class SubTask : MonoBehaviour {
    public string description;
    public GameObject onCompleteObject;
    public SubTaskAction onCompleteAction;
    public List<SubTask> priorSubTasks;
    public int amountToComplete = 1;
    public int amountCompleted = 0;
    bool isCompleted;
    private bool runTask = true;
	public	GameObject sound;
	public	AudioManager AM;

    public bool IsCompleted
    {
        get {
            return isCompleted;
        }
    }

    public void CompletedIntercation(bool isCompleted, Interactable interctable, GameObject obj)
    {
		sound=GameObject.FindGameObjectWithTag("AudioManager");
		AM=sound.GetComponent<AudioManager>();
        //will be set to completion on interaction
        if (priorSubTasks.Count != 0) {
            runTask = true;
            foreach (SubTask s in priorSubTasks)
            {
                if (!s.isCompleted)
                {
                    runTask = false;
                }
            }
        }

        if (runTask) {
			if(this.tag=="tMopFloor")
				AM.Play("mop");
            amountCompleted++;
            this.HandleCompletion(interctable, obj);

            if (amountCompleted == amountToComplete)
            {
                Debug.Log(description + " Completed");
                this.isCompleted = isCompleted;
            }
        }
    }

    public SubTaskAction OnCompleteAction
    {
        get {return onCompleteAction;}
        set {onCompleteAction = value;}
    }

    // Use this for initialization
    void HandleCompletion(Interactable interctable, GameObject obj)
    {
        if (OnCompleteAction == SubTaskAction.ReplaceInScene)
        {
            foreach (MeshCollider mc in interctable.GetComponents<MeshCollider>())
            {
                mc.enabled = false;
            }
            foreach (MeshRenderer mr in interctable.GetComponents<MeshRenderer>())
            {
                mr.enabled = false;
            }
            foreach (Interactable mr in interctable.GetComponents<Interactable>())
            {
                mr.enabled = false;
            }
            foreach (MeshCollider mc in interctable.GetComponentsInChildren<MeshCollider>())
            {
                mc.enabled = false;
            }
            foreach (MeshRenderer mr in interctable.GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
            if (onCompleteObject)
            {
                onCompleteObject.SetActive(true);
            }
            //then give it the right positions and stuff
        }
        else if (OnCompleteAction == SubTaskAction.Destroy)
        {
            if (onCompleteObject)
            {
                Destroy(onCompleteObject);
            }
        }
        else if (OnCompleteAction == SubTaskAction.MakeInteractableVisible)
        {
            foreach (MeshCollider mc in interctable.GetComponents<MeshCollider>())
            {
                mc.enabled = true;
            }
            foreach (MeshRenderer mr in interctable.GetComponents<MeshRenderer>())
            {
                mr.enabled = true;
            }
            foreach (MeshCollider mc in interctable.GetComponentsInChildren<MeshCollider>())
            {
                mc.enabled = true;
            }
            foreach (MeshRenderer mr in interctable.GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = true;
            }
            if (obj) {
                Destroy(obj);
            }
        }
        else if (OnCompleteAction == SubTaskAction.MakeInteractableInvisible)
        {
            foreach (MeshCollider mc in interctable.GetComponents<MeshCollider>())
            {
                mc.enabled = false;
            }
            foreach (MeshRenderer mr in interctable.GetComponents<MeshRenderer>())
            {
                mr.enabled = false;
            }
            foreach (MeshCollider mc in interctable.GetComponentsInChildren<MeshCollider>())
            {
                mc.enabled = false;
            }
            foreach (MeshRenderer mr in interctable.GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
        }
    } 
}
