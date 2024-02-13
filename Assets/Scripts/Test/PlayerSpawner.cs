using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined, IPlayerLeft
{
    public static PlayerSpawner Instance;

    public NetworkRunner runner;
    public GameObject VRPlayerPrefab;
    public GameObject SpecPrefab;
    private Dictionary<PlayerRef, NetworkObject> playerNetworkObjects = new Dictionary<PlayerRef, NetworkObject>();

    private void Awake()
    {
        // Ensure only one instance of this object exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        name = "[Network Runner VR]";
    }

    private void Start()
    {
        // Check if runner is already running to avoid duplicates
        if (runner == null || !runner.IsRunning)
        {
            runner = GetComponent<NetworkRunner>();
            if (runner == null)
            {
                runner = gameObject.AddComponent<NetworkRunner>();
            }

            runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                SessionName = SystemInfo.deviceName,
                Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
                PlayerCount = 2
            });
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (Runner.IsServer)
        {
            NetworkObject spawnedNetworkObject = null;
            if (player == Runner.LocalPlayer && Runner.SessionInfo.PlayerCount <= 1)
            {
                Debug.Log("PlayerSpawner: VRPlayer[" + player.PlayerId + "] join");
                spawnedNetworkObject = Runner.Spawn(VRPlayerPrefab, Vector3.zero, Quaternion.identity, player);
            }
            else
            {
                Debug.Log("PlayerSpawner: SpectatorPlayer[" + player.PlayerId + "] join");
                spawnedNetworkObject = Runner.Spawn(SpecPrefab, Vector3.zero, Quaternion.identity, player);
            }

            if (spawnedNetworkObject != null)
            {
                DontDestroyOnLoad(spawnedNetworkObject.gameObject);
                playerNetworkObjects[player] = spawnedNetworkObject;
            }
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("Player left: " + player.PlayerId);
        if (playerNetworkObjects.TryGetValue(player, out NetworkObject networkObject))
        {
            DestroyPlayerNetworkObject(networkObject);
            playerNetworkObjects.Remove(player);
        }
    }
    
    private void DestroyPlayerNetworkObject(NetworkObject networkObject)
    {
        if (networkObject != null && networkObject.HasStateAuthority)
        {
            networkObject.Runner.Despawn(networkObject);
        }
    }
}