using UnityEngine;

public enum SubTaskAction
{
    GivePlayer,
    SpawnInScene,
    Destroy,
}

public class SubTask : MonoBehaviour {
    public string description;
    public GameObject onCompleteObject;
    SubTaskAction onCompleteAction;
    bool isCompleted;

    public bool IsCompleted
    {
        get {
            return isCompleted;
        }
        set {
            //will be set to completion on interaction
            isCompleted = value;
            this.HandleCompletion();
        }
    }

    public SubTaskAction OnCompleteAction
    {
        get {return onCompleteAction;}
        set {onCompleteAction = value;}
    }

    // Use this for initialization
    void HandleCompletion() {
        if (OnCompleteAction == SubTaskAction.GivePlayer) {

        }
        else if (OnCompleteAction == SubTaskAction.SpawnInScene) {

        }
        else if (OnCompleteAction == SubTaskAction.Destroy) {
        }
	}
}
