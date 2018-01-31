using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

public class MentorScoreMessage : MessageBase
{
    public int playerMSG;
    public int scoreTypeMSG;
}

public class StateMessage : MessageBase
{
    public bool state;
}

public class LevelManager : MonoBehaviour {
    public ScoreController scoreController;

    // Use this for initialization
    void Start () {
        StateMessage state = new StateMessage();
        state.state = true;
        NetworkServer.SendToAll((short)OutgoingRequests.IsGameState, state);
        NetworkServer.RegisterHandler((short)IngoingRequests.IncreaseScore, OnIncreaseScore);
    }

    public void OnIncreaseScore(NetworkMessage netMsg)
    {
        MentorScoreMessage message = netMsg.ReadMessage<MentorScoreMessage>();
        scoreController.IncrementScore(message.playerMSG, message.scoreTypeMSG);
    }
}
