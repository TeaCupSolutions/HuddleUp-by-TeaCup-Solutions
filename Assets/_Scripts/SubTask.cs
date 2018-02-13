using System;
using UnityEngine;

public enum SubTaskAction
{
    GivePlayer,
    SpawnInScene,
    Destroy,
    MakeInteractableVisible,
}

public class SubTask : MonoBehaviour {
    public string description;
    public GameObject onCompleteObject;
    public SubTaskAction onCompleteAction;
    public int amountToComplete = 1;
    public int amountCompleted = 0;
    bool isCompleted;

    public bool IsCompleted
    {
        get {
            return isCompleted;
        }
    }

    public void CompletedIntercation(bool isCompleted, Interactable interctable, GameObject obj)
    {
        //will be set to completion on interaction
        amountCompleted++;
        this.HandleCompletion(interctable, obj);

        if (amountCompleted == amountToComplete)
        {
            Debug.Log(description + " Completed");
            this.isCompleted = isCompleted;
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
    }
    
}
