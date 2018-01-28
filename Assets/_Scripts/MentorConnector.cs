using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MentorConnector : MonoBehaviour
{
    enum OutgoingRequests
    {
        IsGameState = 100,
        IsMenuState = 101,
        IsReplayState = 102,
    }

    bool isAtStartup = true;
    void Update()
    {
        if (isAtStartup)
        {
            NetworkServer.Listen(8888);
            NetworkServer.RegisterHandler(MsgType.Ready, OnPlayerReadyMessage);

            isAtStartup = false;
        }
    }

    public void OnPlayerReadyMessage(NetworkMessage netMsg)
    {
        // TODO: create player and call PlayerIsReady()
    }
}