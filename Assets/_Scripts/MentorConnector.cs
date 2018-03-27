using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MentorConnector : MonoBehaviour
{
    private void Start()
    {
        if (StaticValuesNamespace.StaticValues.IsAtStartup)
        {
            NetworkServer.Listen(8888);
            StaticValuesNamespace.StaticValues.IsAtStartup = false;
        }

        StateMessage state = new StateMessage();
        NetworkServer.SendToAll((short)OutgoingRequests.IsMenuState, state);
    }
}