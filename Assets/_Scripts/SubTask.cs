using System;
using UnityEngine;

public enum SubTaskAction
{
    GivePlayer,
    SpawnInScene,
    Destroy,
    MakeInteractableVisible,
    MakeInteractableInvisible,
}

public class SubTask : MonoBehaviour {
    public string description;
    public GameObject onCompleteObject;
    public SubTaskAction onCompleteAction;
    public SubTask priorSubTask;
    public int amountToComplete = 1;
    public int amountCompleted = 0;
    bool isCompleted;
    private bool runTask = false;

    public bool IsCompleted
    {
        get {
            return isCompleted;
        }
    }

    public void CompletedIntercation(bool isCompleted, Interactable interctable, GameObject obj)
    {
        //will be set to completion on interaction
        if (priorSubTask)
        {
            if (priorSubTask.isCompleted)
            {
                runTask = true;
            }
        }
        else
        {
            runTask = true;
        }

        if (runTask) {
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
        if (OnCompleteAction == SubTaskAction.GivePlayer)
        {
            Instantiate(onCompleteObject);
            //then magic it to the player somehow
        }
        else if (OnCompleteAction == SubTaskAction.SpawnInScene)
        {
            Instantiate(onCompleteObject);
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
