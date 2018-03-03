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

    enum IngoingRequests
    {
        IncreaseScore = 151,
    }

    bool isAtStartup = true;
    void Update()
    {
        if (isAtStartup)
        {
            NetworkServer.Listen(8888);
            isAtStartup = false;
        }
    }
}