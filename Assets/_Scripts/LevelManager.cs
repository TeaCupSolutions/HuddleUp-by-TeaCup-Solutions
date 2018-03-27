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
    ReplayAction = 152,
}

public class MentorScoreMessage : MessageBase
{
    public int playerMSG;
    public int scoreTypeMSG;
}

public class ReplayActionMessage : MessageBase
{
    public int action;
}

public class StateMessage : MessageBase
{
    public bool state;
}

public class LevelManager : MonoBehaviour {
    public ScoreController scoreController;
    public ReplayIngameMenu replayController;

    // Use this for initialization
    void Start () {
        StateMessage state = new StateMessage();
        state.state = true;
        if (StaticValuesNamespace.StaticValues.IsReplay)
        {
            NetworkServer.SendToAll((short)OutgoingRequests.IsReplayState, state);
            NetworkServer.RegisterHandler((short)IngoingRequests.ReplayAction, OnReplayAction);
        }
        else
        {
            NetworkServer.SendToAll((short)OutgoingRequests.IsGameState, state);
            NetworkServer.RegisterHandler((short)IngoingRequests.IncreaseScore, OnIncreaseScore);
        }
    }

    public void OnIncreaseScore(NetworkMessage netMsg)
    {
        MentorScoreMessage message = netMsg.ReadMessage<MentorScoreMessage>();
        scoreController.IncrementScore(message.playerMSG, message.scoreTypeMSG);
    }

    public void OnReplayAction(NetworkMessage netMsg)
    {
        ReplayActionMessage message = netMsg.ReadMessage<ReplayActionMessage>();
        replayController.replayAction(message.action);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        StateMessage state = new StateMessage();
        if (StaticValuesNamespace.StaticValues.IsReplay)
        {
            NetworkServer.SendToAll((short)OutgoingRequests.IsReplayState, state);
        }
        else
        {
            NetworkServer.SendToAll((short)OutgoingRequests.IsGameState, state);
        }
    }
}
