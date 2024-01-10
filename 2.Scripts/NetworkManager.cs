using Fusion;
using Fusion.Sockets;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    NetworkRunner _runner;
    [SerializeField] NetworkPrefabRef playerPrefab0;
    [SerializeField] NetworkPrefabRef playerPrefab1;
    [SerializeField] NetworkPrefabRef playerPrefab2;
    [SerializeField] NetworkPrefabRef playerPrefab3;
    [SerializeField] NetworkPrefabRef playerPrefab4;
    [SerializeField] NetworkPrefabRef playerPrefab5;
    [SerializeField] NetworkPrefabRef playerPrefab6;
    [SerializeField] NetworkPrefabRef playerPrefab7;
    [SerializeField] NetworkPrefabRef playerPrefab8;
    [SerializeField] NetworkPrefabRef playerPrefab9;
    NetworkPrefabRef selectPrefab;
    public Transform spawnPos;
    public GameObject connectWindow, disconBtn, chatWindow;
    public InputField inputName;
    public ChatManager chatManager;
    public Button connectBtn;
    PlayerRef savePlayer;
    public UserData userData;

    private void Awake()
    {
        connectWindow.SetActive(true);
        disconBtn.SetActive(false);
        chatManager.gameObject.SetActive(false);
        chatWindow.SetActive(false);  // 채팅창 끄기고 시작
    }

    public async void ConnetcBtn()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;
        connectBtn.interactable = false;
        await StartMode(GameMode.Shared);
    }

    public void DisconBtn()
    {
        _runner.Shutdown();
        chatManager.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (player != runner.LocalPlayer) return;
        NetworkObject networkPlayerObj = runner.Spawn(SelectPlayer(), spawnPos.position, Quaternion.identity, player);
        savePlayer = player;
        //플레이어가 자신의 캐릭터만 생성을 시키면 다른 유저들에겐 알아서 동기화됨.
        connectWindow.SetActive(false );
        disconBtn.SetActive(true );
        chatManager.gameObject.SetActive(true);  
    }

    NetworkPrefabRef SelectPlayer()
    {
        if (userData.charIndex == 0)
            selectPrefab = playerPrefab0;
        else if (userData.charIndex == 1)
            selectPrefab = playerPrefab1;
        else if (userData.charIndex == 2)
            selectPrefab = playerPrefab2;
        else if (userData.charIndex == 3)
            selectPrefab = playerPrefab3;
        else if (userData.charIndex == 4)
            selectPrefab = playerPrefab4;
        else if (userData.charIndex == 5)
            selectPrefab = playerPrefab5;
        else if (userData.charIndex == 6)
            selectPrefab = playerPrefab6;
        else if (userData.charIndex == 7)
            selectPrefab = playerPrefab7;
        else if (userData.charIndex == 8)
            selectPrefab = playerPrefab8;
        else if (userData.charIndex == 9)
            selectPrefab = playerPrefab9;

        return (selectPrefab);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (chatWindow.activeInHierarchy  && chatWindow.GetComponentInChildren<InputField>().isFocused) return; //입 력중일때 조작 방지

        var data = new NetworkInputData();
        if (Input.GetAxis("Horizontal") != 0)
        {
            data.dir += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            data.dir += new Vector3(0, 0, Input.GetAxis("Vertical"));
        }
        if(Input.GetKey(KeyCode.Z)) data.fire =true;
        if(Input.GetKeyUp(KeyCode.Z)) data.fire =false;
        if (Input.GetKey(KeyCode.Space)) data.isJump = true;
        if (Input.GetKeyUp(KeyCode.Space)) data.isJump = false;

        input.Set(data);


    }



    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        connectBtn.interactable = true;
    }

    #region RunnerCallbacks
    public void OnConnectedToServer(NetworkRunner runner)
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

    async Task StartMode(GameMode mode)
    {
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "TestSession",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void Teleport(Vector3 exitPortal)
    {
        NetworkObject networkPlayerObj = _runner.Spawn(SelectPlayer(), exitPortal, Quaternion.identity, savePlayer);
    }


}
