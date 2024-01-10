using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour,INetworkRunnerCallbacks
{
    NetworkRunner _runner;
    [SerializeField] NetworkPrefabRef _playerPrefab; // 플레이어의 프리팹
    Dictionary<PlayerRef,NetworkObject> _spawnedPlayer = new Dictionary<PlayerRef,NetworkObject>();

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers)*3,1,0);
        NetworkObject networkPlayerObj = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
        _spawnedPlayer.Add(player, networkPlayerObj);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if(_spawnedPlayer.TryGetValue(player,out NetworkObject networkPlayerObj))
        {
            runner.Despawn(networkPlayerObj);
            _spawnedPlayer.Remove(player);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();

        if(Input.GetAxis("Horizontal") != 0)
        {
            data.dir += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            data.dir += new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        input.Set(data);
    }


    #region RunnerCallbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
       
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
       
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }


    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
       
    }


    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
 
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
      
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
     
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
       
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
      
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
     
    }

    #endregion

    async void StartMode(GameMode mode)
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestSession",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    void OnGUI()
    {
        if(_runner == null)
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
            {
                StartMode(GameMode.Host);
            }
            if (GUI.Button(new Rect(0, 40, 200, 40), "Client"))
            {
                StartMode(GameMode.Client);
            }
            if (GUI.Button(new Rect(0, 80, 200, 40), "Host of Client"))
            {
                StartMode(GameMode.AutoHostOrClient);
            }
        }
    }


}
