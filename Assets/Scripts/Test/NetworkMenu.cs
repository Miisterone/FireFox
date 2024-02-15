using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkMenu : MonoBehaviour, INetworkRunnerCallbacks
{
    public static NetworkMenu instance;
    public NetworkRunner runner;
    //public GameObject SpecPrefab;
    //public Dictionary<PlayerRef, NetworkObject> playerNetworkObjects = new Dictionary<PlayerRef, NetworkObject>();
    
    [Header("Session List")] 
    public GameObject roomListCanvas;
    public Button refreshButton;
    public Transform sessionListContent;
    public GameObject sessionEntryPrefab;
    private List<SessionInfo> _sessions = new List<SessionInfo>();
    
    private void Awake()
    {
        if (instance == null)
            instance = this;

        name = "[Network Runner Mobile]";
        runner = GetComponent<NetworkRunner>();
        roomListCanvas = GameObject.Find("RoomListCanvas");
        refreshButton = GameObject.Find("Refresh Button").GetComponent<Button>();
        sessionListContent = GameObject.Find("Session List Content").transform;
        ConnectToLobby();
    }
    public void ConnectToLobby()
    {
        if (roomListCanvas != null)
            roomListCanvas.SetActive(true);
        
        if (runner == null)
            runner = gameObject.AddComponent<NetworkRunner>();
        
        runner.JoinSessionLobby(SessionLobby.ClientServer);
    }
    public async void ConnectToSession(string sessionName)
    {
        roomListCanvas.SetActive(false);
        if (runner == null)
            runner = gameObject.AddComponent<NetworkRunner>();

        var startGameResult = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = sessionName,
            PlayerCount = 2,
        });

        if (startGameResult.Ok)
        {
            // Load the scene after confirming the session start is successful
            SceneManager.LoadSceneAsync("Menu");
        }
        else
        {
            Debug.LogError("Failed to start game session: " + startGameResult.ShutdownReason);
            // Handle failed session start (e.g., return to menu, display error message)
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //NetworkObject spawnedNetworkObject = null;
        Debug.Log("NetworkMenu: OnPlayerJoined");
        // Debug.Log("NetworkMenu: Spawn SpectatorPlayer[" + player.PlayerId + "] join");
        // spawnedNetworkObject = runner.Spawn(SpecPrefab, Vector3.zero, Quaternion.identity, player);
        //
        // if (spawnedNetworkObject != null)
        // {
        //     DontDestroyOnLoad(spawnedNetworkObject.gameObject);
        //     playerNetworkObjects[player] = spawnedNetworkObject;
        // }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
       Debug.Log("OnInput");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        throw new NotImplementedException();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutdown");
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("Disconnected from server");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        throw new NotImplementedException();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        throw new NotImplementedException();
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        throw new NotImplementedException();
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _sessions.Clear();
        _sessions = sessionList;
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        throw new NotImplementedException();
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        throw new NotImplementedException();
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        throw new NotImplementedException();
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadDone");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
       Debug.Log("OnSceneLoadStart");
    }
    
    public void RefreshSessionListUI()
    {
        //clears our Session List UI
        foreach (Transform child in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (SessionInfo session in _sessions)
        {
            if (session.IsVisible)
            {
                GameObject entry = GameObject.Instantiate(sessionEntryPrefab, sessionListContent);
                SessionEntryPrefab script = entry.GetComponent<SessionEntryPrefab>();
                script.sessionName.text = session.Name;
                script.playerCount.text = session.PlayerCount + "/" + session.MaxPlayers;
                    
                if (session.IsOpen == false || session.PlayerCount >= session.MaxPlayers)
                {
                    script.joinButton.interactable = false;
                }
                else
                {
                    script.joinButton.interactable = true;
                }
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        if (runner != null)
        {
            runner.Shutdown();
        }
    }
}
